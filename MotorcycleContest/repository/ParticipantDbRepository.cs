using System.Collections.Generic;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class ParticipantDbRepository : AbstractRepository<long, Participant>, IParticipantRepository
    {
        public ParticipantDbRepository(IValidator<Participant> validator) : base(validator)
        {
        }

        public List<Participant> FilterByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public List<Participant> FilterByMotorcycle(Motorcycle motorcycle)
        {
            throw new System.NotImplementedException();
        }

        public List<Participant> FilterByTeam(Team team)
        {
            throw new System.NotImplementedException();
        }
    }
}