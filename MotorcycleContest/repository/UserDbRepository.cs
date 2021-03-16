using System.Collections.Generic;
using log4net;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class UserDbRepository : IUserRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("UserDbRepository");
        
        public UserDbRepository()
        {
            Log.Info("Creating UserDbRepository");
        }
        public int Size()
        {
            throw new System.NotImplementedException();
        }

        public void Save(User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, User entity)
        {
            throw new System.NotImplementedException();
        }

        public User FindOne(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public List<User> FilterByUsername(string username)
        { 
            Log.InfoFormat("Entering FilterByUsername - User");
            
            var con = DbUtils.GetConnection();
            var users = new List<User>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Users WHERE Username = @usrn";

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@usrn";
                paramUsername.Value = username;
                comm.Parameters.Add(paramUsername);
                
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var password = dataR.GetString(2);

                        var user = new User(username, password);
                        user.Id = id;
                        
                        users.Add(user);
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByRace with value {0}", users);
            return users;
        }
    }
}