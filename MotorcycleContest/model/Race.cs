using System.Collections.Generic;

namespace MotorcycleContest.model
{
    public class Race : Entity<long>
    {
        public List<long> ParticipantsIds { get; set; }
        public EngineCapacity RequiredEngineCapacity { get; set; }

        public Race(List<long> participantsIds, EngineCapacity requiredEngineCapacity)
        {
            ParticipantsIds = participantsIds;
            RequiredEngineCapacity = requiredEngineCapacity;
        }
    }
}