using System.Collections.Generic;
using Domain.model;

namespace Persistance.interfaces
{
    public interface IRaceRepository : IRepository<int, Race>
    {
        List<Race> FilterByParticipants(Participant participant);
        
        List<Race> FilterByRequiredEngineCapacity(EngineCapacity engineCapacity);
        
        Race FilterByName(string name);
    }
}

