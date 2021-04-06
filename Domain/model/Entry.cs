using System;

namespace Domain.model
{
    [Serializable]
    public class Entry : Entity<int>
    {
        public int RaceId { get; set; }
        public int ParticipantId { get; set; }

        public Entry(int raceId, int participantId)
        {
            RaceId = raceId;
            ParticipantId = participantId;
        }

    }
}