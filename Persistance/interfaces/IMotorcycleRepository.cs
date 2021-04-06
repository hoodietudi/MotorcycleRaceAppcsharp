using System.Collections.Generic;
using Domain.model;

namespace Persistance.interfaces
{
    public interface IMotorcycleRepository : IRepository<int, Motorcycle>
    {
        List<Motorcycle> FilterByEngineCapacity(EngineCapacity engineCapacity);

        Motorcycle LastMotorcycleAdded();
    }
}
