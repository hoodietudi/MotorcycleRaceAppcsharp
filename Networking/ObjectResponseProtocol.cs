using System;
using Domain.model;

namespace Networking
{
    public interface Response
    {
        
    }
    
    [Serializable]
    public class OkResponse : Response
    {
		
    }

    [Serializable]
    public class ErrorResponse : Response
    {
        public ErrorResponse(string message)
        {
            this.Message = message;
        }

        public virtual string Message { get; }
    }
    
    public interface UpdateResponse : Response
    {
    }

    [Serializable]
    public class ParticipantEntryAdded : UpdateResponse
    {
        public virtual string RaceName { get; }

        public ParticipantEntryAdded(string raceName)
        {
            this.RaceName = raceName;
        }
    }
    
    [Serializable]
    public class GetRacesResponse : Response
    {
        public RaceDTO[] Races { get; }

        public GetRacesResponse(RaceDTO[] races)
        {
            this.Races = races;
        }
    }
    
    [Serializable]
    public class GetTeamsNamesResponse : Response
    {
        public string[] Names { get; }

        public GetTeamsNamesResponse(string[] names)
        {
            this.Names = names;
        }
    }
    
    [Serializable]
    public class GetTeamParticiapntsResponse : Response
    {
        public ParticipantDTO[] Participants { get; }

        public GetTeamParticiapntsResponse(ParticipantDTO[] participants)
        {
            this.Participants = participants;
        }
    }
    
    [Serializable]
    public class GetRacesByEngineResponse : Response
    {
        public Race[] Races { get; }

        public GetRacesByEngineResponse(Race[] races)
        {
            this.Races = races;
        }
    }
    
    [Serializable]
    public class AddParticipantEntryResponse : Response
    {
        
    }
}