using System;
using System.Collections.Generic;
using Domain.model;
using Domain.model.validators;
using log4net;
using Persistance.interfaces;

namespace Persistance
{
    public class MotorcycleDbRepository : IMotorcycleRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("MotorcycleDbRepository");
        private IValidator<Motorcycle> _validator;
        
        public MotorcycleDbRepository()
        {
            Log.Info("Creating MotorcycleDbRepository");
            _validator = new MotorcycleValidator();
        }
        
        public int Size()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Motorcycle entity)
        {
            Log.InfoFormat("Entring Save - Motorcycle");
            
            _validator.Validate(entity);
            var con = DbUtils.GetConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO Motorcycles (EngineCapacity) VALUES (@cap)";
                var paramEngineCapacity = comm.CreateParameter();
                paramEngineCapacity.ParameterName = "@cap";
                paramEngineCapacity.Value = entity.EngineCapacity.ToString();
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

        public List<Motorcycle> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Motorcycle> FilterByEngineCapacity(EngineCapacity engineCapacity)
        {
            throw new System.NotImplementedException();
        }

        public Motorcycle LastMotorcycleAdded()
        {
            Log.InfoFormat("Entering LastMotorcycleAdded - Motorcycle");
            
            var con = DbUtils.GetConnection();
            Motorcycle motorcycle = null;
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Motorcycles ORDER BY MotorcycleId DESC LIMIT 1";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(1);
                        string engine = dataR.GetString(0);

                        motorcycle = new Motorcycle((EngineCapacity) Enum.Parse(
                            typeof(EngineCapacity), engine)) {Id = id};
                        _validator.Validate(motorcycle);
                    }
                }
            }
            Log.InfoFormat("Exiting LastMotorcycleAdded with value {0}", motorcycle);
            return motorcycle;
        }
    }
    
}