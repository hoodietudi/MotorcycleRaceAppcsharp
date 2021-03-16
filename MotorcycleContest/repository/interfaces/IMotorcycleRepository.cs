using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
{
    public interface IMotorcycleRepository : IRepository<int, Motorcycle>
    {
        List<Motorcycle> FilterByEngineCapacity(EngineCapacity engineCapacity);
    }
}
