using System;

namespace Domain.model
{
    [Serializable]
    public class ParticipantDTO
    {
        public string Name { get; set; }
        public EngineCapacity EngineCapacity { get; set; }

        public ParticipantDTO(string name, EngineCapacity engineCapacity)
        {
            Name = name;
            EngineCapacity = engineCapacity;
        }
    }
}