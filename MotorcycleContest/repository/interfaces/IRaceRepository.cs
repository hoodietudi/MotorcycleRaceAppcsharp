using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
{
    public interface IRaceRepository : IRepository<int, Race>
    {
        List<Race> FilterByParticipants(Participant participant);
        
        List<Race> FilterByRequiredEngineCapacity(EngineCapacity engineCapacity);
    }
}

