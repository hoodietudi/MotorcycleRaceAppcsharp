using System.Collections.Generic;
using Domain.model;
using Domain.model.validators;
using log4net;
using Persistance.interfaces;

namespace Persistance
{
    public class EntryDbRepository : IEntryRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("EntryDbRepository");
        private readonly IValidator<Entry> _validator;
        
        public EntryDbRepository()
        {
            Log.Info("Creating EntryDbRepository");
            _validator = new EntryValidator();
        }
        
        public int Size()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Entry entity)
        {
            Log.InfoFormat("Entering Save - Entry");
            
            _validator.Validate(entity);
            var con = DbUtils.GetConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO Entry (RaceId, ParticipantId) VALUES (@rId, @pId)";
                
                var paramRaceId = comm.CreateParameter();
                paramRaceId.ParameterName = "@rId";
                paramRaceId.Value = entity.RaceId;
                comm.Parameters.Add(paramRaceId);
                
                var paramParticipantId = comm.CreateParameter();
                paramParticipantId.ParameterName = "@pId";
                paramParticipantId.Value = entity.ParticipantId;
                comm.Parameters.Add(paramParticipantId);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No entry added!");
            }
            Log.InfoFormat("Exit Save - Entry");
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Entry entity)
        {
            throw new System.NotImplementedException();
        }

        public Entry FindOne(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Entry> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Entry> FilterByRace(Race race)
        {
            Log.InfoFormat("Entering FilterByRace");
            var con = DbUtils.GetConnection();
            var entries = new List<Entry>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Entry WHERE RaceId = @id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = race.Id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        int participantId = dataR.GetInt32(2);

                        Entry entry = new Entry(race.Id, participantId) {Id = id};
                        _validator.Validate(entry);
                        
                        entries.Add(entry);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByRace with value {0}", entries);
            return entries;
        }
    }
}