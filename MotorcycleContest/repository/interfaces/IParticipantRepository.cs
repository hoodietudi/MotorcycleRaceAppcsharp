using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
{
    public interface IParticipantRepository : IRepository<long, Participant>
    {
        List<Participant> FilterByName(string name);
        
        List<Participant> FilterByMotorcycle(Motorcycle motorcycle);
        
        List<Participant> FilterByTeam(Team team);
    }
}