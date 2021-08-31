﻿using System;
using System.Net.Sockets;
using System.Threading;
using Services;
using Google.Protobuf;
using Constest.Protocol;
 using Domain.model;

 namespace protobuf
{
    public class ProtoContestWorker : IContestObserver
    {
        private IContestServices _server;
        private TcpClient _connection;

        private NetworkStream _stream;
        private volatile bool _connected;

        public ProtoContestWorker(IContestServices server, TcpClient connection)
        {
            this._server = server;
            this._connection = connection;

            try
            {
                _stream = connection.GetStream();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void Run()
        {
            while (_connected)
            {
                try
                {
                    var request = ContestRequest.Parser.ParseDelimitedFrom(_stream);
                    var response = HandleRequest(request);

                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            try
            {
                _stream.Close();
                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
        
        private void SendResponse(ContestResponse response)
        {
            Console.WriteLine("Sending response: " + response);
            lock (_stream)
            {
                response.WriteDelimitedTo(_stream);
                _stream.Flush();
            }
        }

        private ContestResponse HandleRequest(ContestRequest request)
        {
            // ContestResponse response = null;
            var reqType = request.Type;

            switch (reqType) 
            {
                case ContestRequest.Types.Type.Login:
                {
                    Console.WriteLine("Login request...");
                    Domain.model.User user = ProtoUtils.GetUser(request);

                    try
                    {
                        lock (_server)
                        {
                            _server.Login(user, this);
                        }

                        return ProtoUtils.CreateOkResponse();
                    }
                    catch (ContestException e)
                    {
                        _connected = false;
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }

                case ContestRequest.Types.Type.Logout:
                {
                    Console.WriteLine("Logout request...");
                    Domain.model.User user = ProtoUtils.GetUser(request);
                    
                    try
                    {
                        lock (_server)
                        {
                            _server.Logout(user, this);
                        }

                        _connected = false;
                        return ProtoUtils.CreateOkResponse();
                    }
                    catch (ContestException e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                    
                case ContestRequest.Types.Type.GetRacesRequest:
                {
                    Console.WriteLine("Get races request...");
                    
                    try
                    {
                        Domain.model.RaceDTO[] races;
                        lock (_server)
                        {
                            races = _server.GetRaces();
                        }
                    
                        return ProtoUtils.CreateRacesResponse(races);
                    }
                    catch (ContestException e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                    
                case ContestRequest.Types.Type.GetTeamsNamesRequest:
                {
                    Console.WriteLine("Get teams names request...");
                    
                    try
                    {
                        string[] teamsNames;
                        lock (_server)
                        {
                            teamsNames = _server.GetTeamsNames();
                        }
                    
                        return ProtoUtils.CreateTeamsNamesResponse(teamsNames);
                    }
                    catch (ContestException e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                    
                case ContestRequest.Types.Type.GetTeamParticipantsRequest:
                {
                    Console.WriteLine("Get team participants request...");
                    string team = ProtoUtils.GetTeamName(request);

                    try
                    {
                        Domain.model.ParticipantDTO[] participantDtos;
                        lock (_server)
                        {
                            participantDtos = _server.GetTeamMembers(team);
                        }
                    
                        return ProtoUtils.CreateTeamParticipantsResponse(participantDtos);
                    }
                    catch (ContestException e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                    
                case ContestRequest.Types.Type.GetRacesByEngineRequest:
                {
                    Console.WriteLine("Get races by engine request...");
                    EngineCapacity engine = ProtoUtils.GetRaceEngineCapacity(request);

                    try
                    {
                        Domain.model.Race[] races;
                        lock (_server)
                        {
                            races = _server.GetRacesByEngineCapacity(engine);
                        }
                    
                        return ProtoUtils.CreateRacesByEngineResponse(races);
                    }
                    catch (ContestException e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
                    
                case ContestRequest.Types.Type.AddParticipantEntryRequest:
                {
                    Console.WriteLine("Add participant request...");

                    try
                    {
                        lock (_server)
                        {
                            _server.AddParticipantEntry(request.Name,
                                request.Team, request.EngineCapacity,
                                request.RaceName);
                        }
                        
                        return ProtoUtils.CreateOkResponse();
                    }
                    catch (ContestException e)
                    {
                        return ProtoUtils.CreateErrorResponse(e.Message);
                    }
                }
            }

            return null;
        }
        
        public void ParticipantEntryAdded(string raceName)
        {
            Console.WriteLine("Participant added at race: " + raceName);
            try
            {
                SendResponse(ProtoUtils.CreateParticipantAddedResponse(raceName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}