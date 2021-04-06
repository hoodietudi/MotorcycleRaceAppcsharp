using System;
using System.Collections.Generic;
using Domain.model;
using Domain.model.validators;
using log4net;
using Persistance.interfaces;

namespace Persistance
{
    public class ParticipantDbRepository : IParticipantRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("ParticipantDbRepository");
        private IValidator<Participant> _validator;
        public ParticipantDbRepository()
        {
            Log.Info("Creating ParticipantDbRepository");
            _validator = new ParticipantValidator();
        }
        
        public int Size()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Participant entity)
        {
            Log.InfoFormat("Entering Save - Participant");
            _validator.Validate(entity);
            var con = DbUtils.GetConnection();

            using (var comm = con.CreateCommand())
            {

                comm.CommandText =  "INSERT INTO Participants (Name, MotorcycleId, TeamId) VALUES (@name, @mId, @tId)";
                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = entity.Name;
                comm.Parameters.Add(paramName);

                var paramMid = comm.CreateParameter();
                paramMid.ParameterName = "@mId";
                paramMid.Value = entity.Motorcycle.Id;
                comm.Parameters.Add(paramMid);

                var paramTid = comm.CreateParameter();
                paramTid.ParameterName = "@tId";
                paramTid.Value = entity.Team.Id;
                comm.Parameters.Add(paramTid);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No Participant added!");
            }
            Log.InfoFormat("Exiting Save - Participant");
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Participant entity)
        {
            throw new System.NotImplementedException();
        }

        public Participant FindOne(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Participant> FindAll()
        {
            Log.InfoFormat("Entering FindAll - Participant");
            
            var con = DbUtils.GetConnection();
            var participants = new List<Participant>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Participants";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var name = dataR.GetString(1);
                        var mId = dataR.GetInt32(2);
                        var tId = dataR.GetInt32(3);

                        Motorcycle motorcycle = null;
                        
                        using (var commMotorcycle = con.CreateCommand())
                        {
                            commMotorcycle.CommandText = "SELECT * FROM Motorcycles WHERE MotorcycleId = @mId";
                            var paramMId = commMotorcycle.CreateParameter();
                            paramMId.ParameterName = "@mId";
                            paramMId.Value = mId;
                            commMotorcycle.Parameters.Add(paramMId);

                            using (var dataRM = commMotorcycle.ExecuteReader())
                            {
                                while (dataRM.Read())
                                {
                                    var motorcycleEngineCapacity = dataRM.GetString(0);
                                    
                                    motorcycle = new Motorcycle((EngineCapacity)Enum.Parse(
                                        typeof(EngineCapacity), motorcycleEngineCapacity));
                                    motorcycle.Id = mId;
                                }
                            }
                        }

                        Team team = null;
                        
                        using (var commTeam = con.CreateCommand())
                        {
                            commTeam.CommandText = "SELECT * FROM Teams WHERE TeamId = @tId";
                            var paramTid = commTeam.CreateParameter();
                            paramTid.ParameterName = "@tId";
                            paramTid.Value = tId;
                            commTeam.Parameters.Add(paramTid);

                            using (var dataRM = commTeam.ExecuteReader())
                            {
                                while (dataRM.Read())
                                {
                                    var teamName = dataRM.GetString(1);
                                    
                                    team = new Team(teamName);
                                    team.Id = tId;
                                }
                            }
                        }
                        
                        var participant = new Participant(name, motorcycle, team);
                        _validator.Validate(participant);
                        participants.Add(participant);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByRace with value {0}", participants);
            return participants;
        }

        public List<Participant> FilterByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public List<Participant> FilterByMotorcycle(Motorcycle motorcycle)
        {
            throw new System.NotImplementedException();
        }

        public List<Participant> FilterByTeam(Team team)
        {
            Log.InfoFormat("Entering FilterByTeam - Participant");
            var con = DbUtils.GetConnection();
            var participants = new List<Participant>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Participants WHERE TeamId = @id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = team.Id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var name = dataR.GetString(1);
                        var motorcycleId = dataR.GetInt32(2);

                        Motorcycle motorcycle = null;

                        using (var commMotorcycle = con.CreateCommand())
                        {
                            commMotorcycle.CommandText = "SELECT * FROM Motorcycles WHERE MotorcycleId = @mId";
                            var paramMId = commMotorcycle.CreateParameter();
                            paramMId.ParameterName = "@mId";
                            paramMId.Value = motorcycleId;
                            commMotorcycle.Parameters.Add(paramMId);

                            using (var dataRM = commMotorcycle.ExecuteReader())
                            {
                                while (dataRM.Read())
                                {
                                    var motorcycleEngineCapacity = dataRM.GetString(0);
                                    
                                    motorcycle = new Motorcycle((EngineCapacity)Enum.Parse(
                                        typeof(EngineCapacity), motorcycleEngineCapacity));
                                    motorcycle.Id = motorcycleId;
                                }
                            }
                        }
                        

                        var participant = new Participant(name, motorcycle, team) {Id = id};
                        
                        participants.Add(participant);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByTeam - Participant with value {0}", participants);
            return participants;
        }

        public Participant LastParticipantAdded()
        {
            Log.InfoFormat("Entering FindAll - Participant");
            
            var con = DbUtils.GetConnection();
            Participant participant = null;
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Participants ORDER BY ParticipantId DESC LIMIT 1";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var name = dataR.GetString(1);
                        var mId = dataR.GetInt32(2);
                        var tId = dataR.GetInt32(3);

                        Motorcycle motorcycle = null;
                        
                        using (var commMotorcycle = con.CreateCommand())
                        {
                            commMotorcycle.CommandText = "SELECT * FROM Motorcycles WHERE MotorcycleId = @mId";
                            var paramMId = commMotorcycle.CreateParameter();
                            paramMId.ParameterName = "@mId";
                            paramMId.Value = mId;
                            commMotorcycle.Parameters.Add(paramMId);

                            using (var dataRM = commMotorcycle.ExecuteReader())
                            {
                                while (dataRM.Read())
                                {
                                    var motorcycleEngineCapacity = dataRM.GetString(0);
                                    
                                    motorcycle = new Motorcycle((EngineCapacity)Enum.Parse(
                                        typeof(EngineCapacity), motorcycleEngineCapacity));
                                    motorcycle.Id = mId;
                                }
                            }
                        }

                        Team team = null;
                        
                        using (var commTeam = con.CreateCommand())
                        {
                            commTeam.CommandText = "SELECT * FROM Teams WHERE TeamId = @tId";
                            var paramTid = commTeam.CreateParameter();
                            paramTid.ParameterName = "@tId";
                            paramTid.Value = tId;
                            commTeam.Parameters.Add(paramTid);

                            using (var dataRM = commTeam.ExecuteReader())
                            {
                                while (dataRM.Read())
                                {
                                    var teamName = dataRM.GetString(1);

                                    team = new Team(teamName) {Id = tId};
                                }
                            }
                        }

                        participant = new Participant(name, motorcycle, team) {Id = id};
                        _validator.Validate(participant);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByRace with value {0}", participant);
            return participant;
        }
    }
}