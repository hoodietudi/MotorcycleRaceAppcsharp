using System.Collections.Generic;
using log4net;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class TeamDbRepository : ITeamRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("TeamDbRepository");
        
        public TeamDbRepository()
        {
            Log.Info("Creating TeamDbRepository");
        }
        public int Size()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Team entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Team entity)
        {
            throw new System.NotImplementedException();
        }

        public Team FindOne(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Team> FindAll()
        {
            Log.InfoFormat("Entering FindAll - Team");
            
            var con = DbUtils.GetConnection();
            var teams = new List<Team>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Teams"; ;
                
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var name = dataR.GetString(1);

                        var team = new Team(name) {Id = id};

                        teams.Add(team);
                    }
                }
            }
            Log.InfoFormat("Exiting FindAll - Team, with value {0}", teams);
            return teams;
        }

        public List<Team> FilterByName(string name)
        {
            Log.InfoFormat("Entering FilterByName - Team");
            
            var con = DbUtils.GetConnection();
            var teams = new List<Team>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Teams WHERE TeamName = @n";

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@n";
                paramName.Value = name;
                comm.Parameters.Add(paramName);
                
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);

                        var team = new Team(name) {Id = id};

                        teams.Add(team);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByName - Team, with value {0}", teams);
            return teams;
        }
    }
}