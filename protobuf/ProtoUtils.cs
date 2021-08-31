﻿using System;
using Domain.model;
using Constest.Protocol;
using proto=Constest.Protocol;
 using Race = Domain.model.Race;

 namespace protobuf
{
    public class ProtoUtils
    {
        public static ContestRequest CreateLoginRequest(Domain.model.User user)
        {
            var userDto = new proto.User {Username = user.Username, Passwd = user.Password};
            var request = new ContestRequest{Type = ContestRequest.Types.Type.Login, User = userDto};

            return request;
        }

        public static Domain.model.User GetUser(ContestRequest request)
        {
            var user = new Domain.model.User(request.User.Username,
                request.User.Passwd);
            return user;
        }

        public static ContestResponse CreateOkResponse()
        {
            var response = new ContestResponse{ Type = ContestResponse.Types.Type.Ok };
            return response;
        }
        
        public static ContestResponse CreateErrorResponse(string text)
        {
            var response = new ContestResponse
            {
                Type = ContestResponse.Types.Type.Error,
                Error =  text
            };
            return response;
        }


        public static ContestResponse CreateRacesResponse(RaceDTO[] races)
        {
            var response = new ContestResponse
            {
                Type = ContestResponse.Types.Type.GetRacesResponse
            };

            foreach (var race in races)
            {
                var raceDto = new RaceDto
                {
                    RaceName = race.RaceName,
                    EngineCapacity = race.RequiredEngineCapacity.ToString(),
                    NumberOfParticipants = race.NumberOfParticipants
                };

                response.RacesDto.Add(raceDto);
            }

            return response;
        }

        public static ContestResponse CreateTeamsNamesResponse(string[] teamsNames)
        {
            var response = new ContestResponse
            {
                Type = ContestResponse.Types.Type.GetTeamsNamesResponse
            };

            foreach (var name in teamsNames)
            {
                response.TeamsNames.Add(name);
            }

            return response;
        }

        public static string GetTeamName(ContestRequest request)
        {
            return request.Team;
        }

        public static ContestResponse CreateTeamParticipantsResponse(ParticipantDTO[] participantDtos)
        {
            var response = new ContestResponse
            {
                Type = ContestResponse.Types.Type.GetTeamParticiapntsResponse
            };

            foreach (var participantDto in participantDtos)
            {
                var participant = new proto.Participant
                {
                    ParticipantName = participantDto.Name,
                    EngineCapacity = participantDto.EngineCapacity.ToString()
                };
                
                response.Participants.Add(participant);
            }

            return response;
        }

        public static EngineCapacity GetRaceEngineCapacity(ContestRequest request)
        {
            return (EngineCapacity) Enum.Parse(
                typeof(EngineCapacity), request.EngineCapacity);
        }

        public static ContestResponse CreateRacesByEngineResponse(Race[] races)
        {
            var response = new ContestResponse
            {
                Type = ContestResponse.Types.Type.GetRacesByEngineResponse
            };

            foreach (var race in races)
            {
                var res = new proto.Race
                {
                    RaceName = race.Name,
                    EngineCapacity = race.RequiredEngineCapacity.ToString()
                };
                
                response.Races.Add(res);
            }

            return response;
        }

        public static ContestResponse CreateParticipantAddedResponse(string raceName)
        {
            return new ContestResponse
            {
                Type = ContestResponse.Types.Type.ParticipantEntryAdded,
                RaceName = raceName
            };
        }
    }
}