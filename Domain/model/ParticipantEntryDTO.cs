using System;

namespace Domain.model
{
    [Serializable]
    public class ParticipantEntryDTO
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public string EngineCapacity { get; set; }
        public string RaceName { get; set; }

        public ParticipantEntryDTO(string name, string team, string engineCapacity, string raceName)
        {
            Name = name;
            Team = team;
            EngineCapacity = engineCapacity;
            RaceName = raceName;
        }
    }
}