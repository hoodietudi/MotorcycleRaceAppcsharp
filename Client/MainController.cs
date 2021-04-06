using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Domain.model;
using Services;

namespace Client
{
    public class MainController : IContestObserver
    {
        public event EventHandler<ContestParticipantEventArgs> updateEvent;
        private readonly IContestServices server;
        private User currentUser;

        public MainController(IContestServices server)
        {
            this.server = server;
            currentUser = null;
        }
        public void Login(string username, string password)
        {
            var user = new User(username, password);
            server.Login(user, this);
            Console.WriteLine(@"Login succeded...");
            currentUser = user;
            Console.WriteLine(@"Current user {0}", user);
        }
        
        public void Logout()
        {
            Console.WriteLine(@"Ctrl logout");
            server.Logout(currentUser, this);
            currentUser = null;
        }
        
        public BindingList<RaceDTO> GetDtoRaces()
        {
            var raceList = new BindingList<RaceDTO>();
            var res = server.GetRaces();
            foreach (var raceDto in res)
            {
                raceList.Add(raceDto);
            }

            return raceList;
        }

        public BindingList<string> GetTeamsNames()
        {
            var namesList = new BindingList<string>();
            var res = server.GetTeamsNames();
            foreach (var name in res)
            {
                namesList.Add(name);
            }

            return namesList;
        }

        public List<ParticipantDTO> GetDtoParticipants(string teamName)
        {
            var res = server.GetTeamMembers(teamName);

            return res.ToList();
        }

        public List<Race> RaceByEngineCapacity(EngineCapacity engineCapacity)
        {
            var res =  server.GetRacesByEngineCapacity(engineCapacity);

            return res.ToList();
        }

        public void AddParticipantEntry(string name, string team, string engineCapacity, string raceName)
        {
            server.AddParticipantEntry(name, team, engineCapacity, raceName);
        }
        
        public void ParticipantEntryAdded(string raceName)
        {
            var participantEvent = new ContestParticipantEventArgs(
                ContestParticipantEvent.ParticipantEntryAdded, raceName);
            OnParticipantEvent(participantEvent);
            
        }
        
        protected virtual void OnParticipantEvent(ContestParticipantEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
    }
}