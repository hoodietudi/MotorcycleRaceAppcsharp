using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Domain.model;
using Services;

namespace Networking
{
    public class ContestClientObjectWorker : IContestObserver
    {
	    
	    private readonly IContestServices _server;
	    private readonly TcpClient _connection;

	    private readonly NetworkStream _stream;
	    private readonly IFormatter _formatter;
	    private volatile bool _connected;
	    
	    public ContestClientObjectWorker(IContestServices server, TcpClient connection)
	    {
		    this._server = server;
		    this._connection = connection;
		    try
		    {
				
			    _stream=connection.GetStream();
			    _formatter = new BinaryFormatter();
			    _connected=true;
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e.StackTrace);
		    }
	    }

	    public virtual void run()
	    {
		    while(_connected)
		    {
			    try
			    {
				    var request = _formatter.Deserialize(_stream);
				    var response = HandleRequest((Request)request);
				    if (response!=null)
				    {
					    SendResponse((Response) response);
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
			    Console.WriteLine("Error "+e);
		    }
	    }
        
        public void ParticipantEntryAdded(string raceName)
        {
	        try
	        {
		        SendResponse(new ParticipantEntryAdded(raceName));
	        }
	        catch (Exception e)
	        {
		        throw new ContestException("Error " + e);
	        }
        }
        
        private object HandleRequest(Request request)
		{
			if (request is LoginRequest logReq)
			{
				Console.WriteLine("Login request ...");
				var udto =logReq.User;
				var user =DTOUtils.GetFromDto(udto);
				try
                {
                    lock (_server)
                    {
                        _server.Login(user, this);
                    }
					return new OkResponse();
				}
				catch (ContestException e)
				{
					_connected=false;
					return new ErrorResponse(e.Message);
				}
			}
			
			if (request is LogoutRequest logReq1)
			{
				Console.WriteLine("Logout request");
				var udto = logReq1.User;
				var user = DTOUtils.GetFromDto(udto);
				try
				{
                    lock (_server)
                    {
	                    _server.Logout(user, this);
                    }
					_connected=false;
					return new OkResponse();

				}
				catch (ContestException e)
				{
				   return new ErrorResponse(e.Message);
				}
			}
			
			if (request is GetRacesRequest logReq2)
			{
				Console.WriteLine("RacesDTO request");
				try
				{
					RaceDTO[] races;
					lock (_server)
					{
						races = _server.GetRaces();
					}
					return new GetRacesResponse(races);

				}
				catch (ContestException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			
			if (request is GetTeamsNamesRequest logReq3)
			{
				Console.WriteLine("TeamsNames request");
				try
				{
					string[] names;
					lock (_server)
					{
						names = _server.GetTeamsNames();
					}
					return new GetTeamsNamesResponse(names);

				}
				catch (ContestException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			
			
			if (request is GetTeamParticipantsRequest logReq4)
			{
				Console.WriteLine("TeamParticipant request");
				var teamName = logReq4.TeamName;
				try
				{
					ParticipantDTO[] participants;
					lock (_server)
					{
						participants = _server.GetTeamMembers(teamName);
					}
					return new GetTeamParticiapntsResponse(participants);

				}
				catch (ContestException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			
			if (request is GetRacesByEngineRequest logReq5)
			{
				Console.WriteLine("TeamParticipant request");
				var engineCapacity = logReq5.EngineCapacity;
				try
				{
					Race[] races;
					lock (_server)
					{
						races = _server.GetRacesByEngineCapacity(engineCapacity);
					}
					return new GetRacesByEngineResponse(races);

				}
				catch (ContestException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			
			if (request is AddParticipantEntryRequest logReq6)
			{
				Console.WriteLine("TeamParticipant request");
				var res = logReq6.ParticipantEntryDto;
				try
				{
					lock (_server)
					{
						_server.AddParticipantEntry(res.Name, res.Team,
							res.EngineCapacity, res.RaceName);
					}
					return new AddParticipantEntryResponse();

				}
				catch (ContestException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			
			return null;
		}

	private void SendResponse(Response response)
		{
			Console.WriteLine("sending response "+response);
            _formatter.Serialize(_stream, response);
            _stream.Flush();
		}
	}
}
