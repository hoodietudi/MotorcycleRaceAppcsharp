using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Domain.model;
using Services;

namespace Networking
{
    public class ContestServerObjectProxy : IContestServices
    {
        private readonly string _host;
        private readonly int _port;

        private IContestObserver _client;

        private NetworkStream _stream;
		
        private IFormatter _formatter;
        private TcpClient _connection;

        private readonly Queue<Response> _responses;
        private volatile bool _finished;
        private EventWaitHandle _waitHandle;
        
        public ContestServerObjectProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new Queue<Response>();
        }
        public void Login(User user, IContestObserver client)
        {
            InitializeConnection();
            var userDto = DTOUtils.GetDto(user);
            SendRequest(new LoginRequest(userDto));
            var response = ReadResponse();

            if (response is OkResponse)
            {
                this._client = client;
                return;
            }

            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                CloseConnection();
                throw new ContestException(err.Message);
            }
        }

        public void Logout(User user, IContestObserver client)
        {
            var userDto = DTOUtils.GetDto(user);
            SendRequest(new LogoutRequest(userDto));
            Response response = ReadResponse();
            CloseConnection();

            if (response is ErrorResponse)
            {
                ErrorResponse er = (ErrorResponse)response;
                throw new ContestException(er.Message);
            }
        }

        public RaceDTO[] GetRaces()
        {
            SendRequest(new GetRacesRequest());
            Response response = ReadResponse();

            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                CloseConnection();
                throw new ContestException(err.Message);
            }

            GetRacesResponse resp = (GetRacesResponse) response;
            RaceDTO[] raceDtos = resp.Races;
            return raceDtos;
        }

        public String[] GetTeamsNames()
        {
            SendRequest(new GetTeamsNamesRequest());
            Response response = ReadResponse();
            
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                CloseConnection();
                throw new ContestException(err.Message);
            }

            GetTeamsNamesResponse resp = (GetTeamsNamesResponse) response;
            return resp.Names;
        }

        public ParticipantDTO[] GetTeamMembers(String team)
        {
            SendRequest(new GetTeamParticipantsRequest(team));
            Response response = ReadResponse();
            
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                CloseConnection();
                throw new ContestException(err.Message);
            }

            GetTeamParticiapntsResponse resp = (GetTeamParticiapntsResponse) response;
            return resp.Participants;
        }

        public Race[] GetRacesByEngineCapacity(EngineCapacity engineCapacity)
        {
            SendRequest(new GetRacesByEngineRequest(engineCapacity));
            Response response = ReadResponse();
            
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                CloseConnection();
                throw new ContestException(err.Message);
            }

            GetRacesByEngineResponse resp = (GetRacesByEngineResponse) response;
            return resp.Races;
        }

        public void AddParticipantEntry(string name, string team, string engineCapacity, string raceName)
        {
            SendRequest(new AddParticipantEntryRequest(new ParticipantEntryDTO(name,
                team, engineCapacity, raceName)));
            Response response = ReadResponse();
            
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                CloseConnection();
                throw new ContestException(err.Message);
            }
        }
        
        
        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void StartReader()
        {
            var tw =new Thread(Run);
            tw.Start();
        }
        
        private void HandleUpdate(UpdateResponse update)
        {
            if (update is ParticipantEntryAdded added)
            {
                // Race race = DTOUtils.getFromDTO(added.Race);
                Console.WriteLine("Participant added at race " + added.RaceName);
                try
                {
                    _client.ParticipantEntryAdded(added.RaceName);
                }
                catch (ContestException e)
                {
                    Console.WriteLine(e.StackTrace); 
                }
            }
        }
        public virtual void Run()
        {
            while(!_finished)
            {
                try
                {
                    var response = _formatter.Deserialize(_stream);
                    Console.WriteLine("response received "+response);
                    if (response is UpdateResponse)
                    {
                        HandleUpdate((UpdateResponse)response);
                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error "+e);
                }
					
            }
        }
        
        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream.Close();
                //output.close();
                _connection.Close();
                _waitHandle.Close();
                _client=null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }
        
        private void SendRequest(Request request)
        {
            try
            {
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (Exception e)
            {
                throw new ContestException("Error sending object "+e);
            }
        }
        

        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (_responses)
                {
                    //Monitor.Wait(responses); 
                    response = _responses.Dequeue();
                
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }
    }
}