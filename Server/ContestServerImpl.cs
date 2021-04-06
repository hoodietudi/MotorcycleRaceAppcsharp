using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.model;
using Domain.model.validators;
using Persistance;
using Persistance.interfaces;
using Services;

namespace Server
{
    public class ContestServerImpl : IContestServices
    {
        private readonly IUserRepository _userRepo;
        private RaceDbRepository _raceRepo;
        private EntryDbRepository _entryRepo;
        private TeamDbRepository _teamRepo;
        private ParticipantDbRepository _participantRepo;
        private MotorcycleDbRepository _motorcycleRepo;
        private readonly IDictionary <string, IContestObserver> _loggedClients;


        public ContestServerImpl(IUserRepository userRepo, RaceDbRepository raceRepo, EntryDbRepository entryRepo, TeamDbRepository teamRepo, ParticipantDbRepository participantRepo, MotorcycleDbRepository motorcycleRepo)
        {
            this._userRepo = userRepo;
            this._raceRepo = raceRepo;
            this._entryRepo = entryRepo;
            this._teamRepo = teamRepo;
            this._participantRepo = participantRepo;
            this._motorcycleRepo = motorcycleRepo;
            _loggedClients = new Dictionary<string, IContestObserver>();
        }


        public void Login(User user, IContestObserver client)
        {
            var res = _userRepo.FilterByUsername(user.Username);

            if (res != null && res.Password == user.Password)
            {
                if (_loggedClients.ContainsKey(res.Username))
                {
                    throw new ContestException("User already logged in!");
                }

                _loggedClients.Add(res.Username, client);
            }
            else
            {
                throw new ContestException("Authentification failed!");
            }
        }

        public void Logout(User user, IContestObserver client)
        {
            IContestObserver localClient = _loggedClients[user.Username];
            if (localClient == null)
            {
                throw new ContestException("User " + user.Id + " is not logged in!");
            }

            _loggedClients.Remove(user.Username);
        }

        public RaceDTO[] GetRaces()
        {
            var races = _raceRepo.FindAll();

            return races.Select(race => new RaceDTO(race.Name, race.RequiredEngineCapacity,
                ParticipantsAtRace(race))).ToArray();
        }

        private int ParticipantsAtRace(Race race)
        {
            return _entryRepo.FilterByRace(race).Count;
        }

        public string[] GetTeamsNames()
        {
            var res = new List<string>();

            foreach (var team in _teamRepo.FindAll()) {
                res.Add(team.Name);
            }

            return res.ToArray();
        }

        public ParticipantDTO[] GetTeamMembers(String teamName)
        {
            List<ParticipantDTO> res = new List<ParticipantDTO>();

            Team team = _teamRepo.FilterByName(teamName);
            
            foreach (var participant in _participantRepo.FilterByTeam(team)) {
                res.Add(new ParticipantDTO(participant.Name,
                    participant.Motorcycle.EngineCapacity));
            }

            return res.ToArray();
        }

        public Race[] GetRacesByEngineCapacity(EngineCapacity engineCapacity)
        {
            return _raceRepo.FilterByRequiredEngineCapacity(engineCapacity).ToArray();
        }
        
        private Motorcycle AddMotorcycle(EngineCapacity engineCapacity) {
            Motorcycle motorcycle = new Motorcycle(engineCapacity);
            _motorcycleRepo.Save(motorcycle);
            motorcycle = _motorcycleRepo.LastMotorcycleAdded();
            return motorcycle;
        }
        
        private int GetRaceId(string name)
        {
            return _raceRepo.FilterByName(name).Id;
        }

        public void AddParticipantEntry(string name, string teamName, string engineCapacity, string raceName)
        {
            try {
                var team = _teamRepo.FilterByName(teamName);

                if (team == null) {
                    throw new ContestException("Add Participant Error - team is null");
                }

                var motorcycle = AddMotorcycle((EngineCapacity) Enum.Parse(
                    typeof(EngineCapacity), engineCapacity));

                if (motorcycle == null) {
                    throw new ContestException("Add Participant Error - motorcycle is null");
                }

                _participantRepo.Save(new Participant(name, motorcycle, team));
                int participantId = _participantRepo.LastParticipantAdded().Id;

                int raceId = GetRaceId(raceName);

                _entryRepo.Save(new Entry(raceId, participantId));

                NotifyNewParticipant(raceName);

            } catch (ValidationException ex) {
                throw new ContestException(ex.Message);
            }
        }

        private void NotifyNewParticipant(string raceNmae)
        {
            foreach (var client in _loggedClients.Values)
            {
                if (client != null)
                {
                    Task.Run(() => client.ParticipantEntryAdded(raceNmae));
                }
            }
        }
    }
}