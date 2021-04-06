using System;
using System.Collections.Generic;
using Domain.model;
using Domain.model.validators;
using log4net;
using Persistance.interfaces;

namespace Persistance
{
    public class RaceDbRepository : IRaceRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("RaceDbRepository");
        private IValidator<Race> _validator;
        
        public RaceDbRepository()
        {
            Log.Info("Creating RaceDbRepository");
            _validator = new RaceValidator();
        }
        public int Size()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Race entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Race entity)
        {
            throw new System.NotImplementedException();
        }

        public Race FindOne(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Race> FindAll()
        {
            Log.InfoFormat("Entering FindAll - Race");
            var con = DbUtils.GetConnection();
            var races = new List<Race>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Races";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var requiredEngineCapacity = dataR.GetString(1);
                        var name = dataR.GetString(2);
                        
                        var race = new Race(
                            (EngineCapacity)Enum.Parse(
                                typeof(EngineCapacity), requiredEngineCapacity), name);
                        race.Id = id;
                        _validator.Validate(race);
                        
                        races.Add(race);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByRace with value {0}", races);
            return races;
        }

        public List<Race> FilterByParticipants(Participant participant)
        {
            throw new System.NotImplementedException();
        }

        public List<Race> FilterByRequiredEngineCapacity(EngineCapacity engineCapacity)
        {
            Log.InfoFormat("Entering FindAll - Race");
            var con = DbUtils.GetConnection();
            var races = new List<Race>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Races WHERE RequiredEngineCapacity = @e";

                var paramEngine = comm.CreateParameter();
                paramEngine.ParameterName = "@e";
                paramEngine.Value = engineCapacity.ToString();
                comm.Parameters.Add(paramEngine);
                
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var requiredEngineCapacity = dataR.GetString(1);
                        var name = dataR.GetString(2);

                        var race = new Race(
                            (EngineCapacity) Enum.Parse(
                                typeof(EngineCapacity), requiredEngineCapacity), name) {Id = id};
                        _validator.Validate(race);
                        
                        races.Add(race);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByRace with value {0}", races);
            return races;
        }

        public Race FilterByName(string teamName)
        {
            Log.InfoFormat("Entering FindAll - Race");
            var con = DbUtils.GetConnection();
            Race race = null;
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Races WHERE Name = @e";

                var paramEngine = comm.CreateParameter();
                paramEngine.ParameterName = "@e";
                paramEngine.Value = teamName;
                comm.Parameters.Add(paramEngine);
                
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var requiredEngineCapacity = dataR.GetString(1);
                        var name = dataR.GetString(2);

                        race = new Race(
                            (EngineCapacity) Enum.Parse(
                                typeof(EngineCapacity), requiredEngineCapacity), name) {Id = id};
                        _validator.Validate(race);
                        
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByName with value {0}", race);
            return race;
        }
    }
}