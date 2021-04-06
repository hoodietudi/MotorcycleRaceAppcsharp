using System;
using Domain.model;

namespace Networking
{
    public interface Request
    {
        
    }
    
    [Serializable]
    public class LoginRequest : Request
    {
        public LoginRequest(UserDTO user)
        {
            this.User = user;
        }

        public virtual UserDTO User { get; }
    }
    
    [Serializable]
    public class LogoutRequest : Request
    {
        public LogoutRequest(UserDTO user)
        {
            this.User = user;
        }

        public virtual UserDTO User { get; }
    }
    
    [Serializable]
    public class GetRacesRequest : Request
    {
        public GetRacesRequest()
        {
        }

    }
    
    
    [Serializable] 
    public class GetTeamsNamesRequest : Request
    {
        public GetTeamsNamesRequest(){
        }

    }
    
    
    [Serializable] 
    public class GetTeamParticipantsRequest : Request
    {
        public GetTeamParticipantsRequest(String teamName)
        {
            this.TeamName = teamName;
        }

        public virtual String TeamName { get; }

    }
    
    [Serializable] 
    public class GetRacesByEngineRequest : Request
    {
        public GetRacesByEngineRequest(EngineCapacity engineCapacity)
        {
            this.EngineCapacity = engineCapacity;
        }

        public virtual EngineCapacity EngineCapacity { get; }

    }
    
    [Serializable] 
    public class AddParticipantEntryRequest : Request
    {
        public AddParticipantEntryRequest(ParticipantEntryDTO participantEntryDto)
        {
            this.ParticipantEntryDto = participantEntryDto;
        }

        public virtual ParticipantEntryDTO ParticipantEntryDto { get; }

    }
}