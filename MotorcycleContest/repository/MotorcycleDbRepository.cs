using System.Collections.Generic;
using log4net;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;


namespace MotorcycleContest.repository
{
    public class MotorcycleDbRepository : IMotorcycleRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("MotorcycleDbRepository");
        
        public MotorcycleDbRepository()
        {
            Log.Info("Creating MotorcycleDbRepository");
        }
        
        public int Size()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Motorcycle entity)
        {
            Log.InfoFormat("Entring Save - Motorcycle");
            var con = DbUtils.GetConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO Motorcycles (EngineCapacity) VALUES (@cap)";
                var paramEngineCapacity = comm.CreateParameter();
                paramEngineCapacity.ParameterName = "@cap";
                paramEngineCapacity.Value = entity.EngineCapacity;
                comm.Parameters.Add(paramEngineCapacity);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No motorcycle added!");
            }
            Log.InfoFormat("Exit Save - Motorcycle");
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Motorcycle entity)
        {
            throw new System.NotImplementedException();
        }

        public Motorcycle FindOne(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Motorcycle> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Motorcycle> FilterByEngineCapacity(EngineCapacity engineCapacity)
        {
            throw new System.NotImplementedException();
        }
    }
    
}