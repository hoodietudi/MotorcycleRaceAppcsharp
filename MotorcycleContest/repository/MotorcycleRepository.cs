using System.Collections.Generic;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class MotorcycleRepository : AbstractRepository<long, Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(IValidator<Motorcycle> validator) : base(validator)
        {
        }

        public List<Motorcycle> FilterByEngineCapacity(EngineCapacity engineCapacity)
        {
            throw new System.NotImplementedException();
        }
    }
}