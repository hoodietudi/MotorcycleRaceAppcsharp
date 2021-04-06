using System;

namespace Domain.model
{
    [Serializable]
    public class RaceDTO
    {
        public string RaceName { get; set; }
        public EngineCapacity RequiredEngineCapacity { get; set; }
        public int NumberOfParticipants { get; set; }

        public RaceDTO(string raceName, EngineCapacity requiredEngineCapacity, int numberOfParticipants)
        {
            RaceName = raceName;
            RequiredEngineCapacity = requiredEngineCapacity;
            NumberOfParticipants = numberOfParticipants;
        }
    }
}