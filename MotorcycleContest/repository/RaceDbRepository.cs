using System.Collections.Generic;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class RaceDbRepository : AbstractRepository<long, Race>, IRaceRepository
    {
        public RaceDbRepository(IValidator<Race> validator) : base(validator)
        {
        }

        public List<Race> FilterByParticipants(Participant participant)
        {
            throw new System.NotImplementedException();
        }

        public List<Race> FilterByRequiredEngineCapacity(EngineCapacity engineCapacity)
        {
            throw new System.NotImplementedException();
        }
    }
}