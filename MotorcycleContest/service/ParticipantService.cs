using System;
using System.Collections.Generic;
using Domain.model;
using Persistance;

namespace MotorcycleContest.service
{
    public class ParticipantService
    {
        private readonly ParticipantDbRepository _repository;

        public ParticipantService(ParticipantDbRepository repository)
        {
            _repository = repository;
        }
        
        public List<Participant> GetAllParticipants() {
            return _repository.FindAll();
        }

        public List<Participant> FilterByTeam(Team team) {
            return _repository.FilterByTeam(team);
        }

        public Participant AddParticipant(string name, Team team, Motorcycle motorcycle) {
            var participant = new Participant(name, motorcycle, team);
            _repository.Save(participant);
            participant = _repository.LastParticipantAdded();
            return participant;
        }
    }
}