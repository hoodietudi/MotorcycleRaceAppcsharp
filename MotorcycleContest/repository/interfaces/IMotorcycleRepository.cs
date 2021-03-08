using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
{
    public interface IMotorcycleRepository : IRepository<long, Motorcycle>
    {
        List<Motorcycle> FilterByEngineCapacity(EngineCapacity engineCapacity);
    }
}
