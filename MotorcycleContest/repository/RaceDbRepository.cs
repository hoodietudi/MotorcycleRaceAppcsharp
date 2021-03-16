using System;
using System.Collections.Generic;
using log4net;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class RaceDbRepository : IRaceRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("RaceDbRepository");
        
        public RaceDbRepository()
        {
            Log.Info("Creating RaceDbRepository");
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

        public IEnumerable<Race> FindAll()
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
            throw new System.NotImplementedException();
        }
    }
}