using System;

namespace Client
{
    public enum ContestParticipantEvent
    {
        ParticipantEntryAdded
    }
    
    public class ContestParticipantEventArgs : EventArgs
    {
        private readonly ContestParticipantEvent participantEvent;
        private readonly Object data;

        public ContestParticipantEventArgs(ContestParticipantEvent userEvent, object data)
        {
            this.participantEvent = participantEvent;
            this.data = data;
        }
        
        public ContestParticipantEvent ParticipantEventType => participantEvent;

        public object Data => data;
    }
}