using System.Collections.Generic;
using Domain.model;

namespace Persistance.interfaces
{
    public interface IParticipantRepository : IRepository<int, Participant>
    {
        List<Participant> FilterByName(string name);
        
        List<Participant> FilterByMotorcycle(Motorcycle motorcycle);
        
        List<Participant> FilterByTeam(Team team);

        Participant LastParticipantAdded();
    }
}