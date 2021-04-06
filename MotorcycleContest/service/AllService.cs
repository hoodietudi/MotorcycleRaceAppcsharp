using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Domain.model;

namespace MotorcycleContest.service
{
    public class AllService
    {
        private readonly UserService _userService;
        private readonly RaceService _raceService;
        private readonly EntryService _entryService;
        private readonly TeamService _teamService;
        private readonly ParticipantService _participantService;
        private readonly MotorcycleService _motorcycleService;

        public AllService(UserService userService, RaceService raceService, EntryService entryService, TeamService teamService, ParticipantService participantService, MotorcycleService motorcycleService)
        {
            _userService = userService;
            _raceService = raceService;
            _entryService = entryService;
            _teamService = teamService;
            _participantService = participantService;
            _motorcycleService = motorcycleService;
        }

        public User VerifyUser (string username, string password) {
            var user = _userService.GetUser(username);

            if (user == null) {
                return null;
            }

            return user.Password == password ? user : null;
        }

        public BindingList<RaceDTO> GetDtoRaces()
        {
            var races = _raceService.GetAllRaces();
            var res = new BindingList<RaceDTO>();

            foreach (var race in races)
            {
                res.Add(new RaceDTO(race.Name, race.RequiredEngineCapacity, _entryService.ParticipantsAtRace(race)));
            }

            return res;
        }
        
        public List<string> GetTeamsNames() {
            var res = new List<string>();

            foreach (var team in _teamService.GetAllTeams()) {
                res.Add(team.Name);
            }

            return res;
        }

        public List<ParticipantDTO> GetDtoParticipants(string teamName)
        {
            var res = new List<ParticipantDTO>();

            var team = _teamService.FilterByName(teamName);

            foreach (var participant in _participantService.FilterByTeam(team))
            {
                res.Add(new ParticipantDTO(participant.Name,
                    participant.Motorcycle.EngineCapacity));
            }

            return res;
        }

        public List<Race> RaceByEngineCapacity(EngineCapacity engineCapacity)
        {
            return _raceService.FilterByEngineCapacity(engineCapacity);
        }

        public Participant AddParticipant(string name, string teamName, string engineCapacity)
        {
            var team = _teamService.FilterByName(teamName);

            var motorcycle = _motorcycleService.AddMotorcycle((EngineCapacity) Enum.Parse(
                typeof(EngineCapacity), engineCapacity));

            return _participantService.AddParticipant(name, team, motorcycle);
        }

        public void AddEntry(Participant participant, Race race)
        {
            _entryService.AddEntry(new Entry(
                race.Id, participant.Id));
        }
    }
}