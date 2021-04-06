using System.Collections.Generic;
using Domain.model;
using Domain.model.validators;
using log4net;
using Persistance.interfaces;

namespace Persistance
{
    public class UserDbRepository : IUserRepository
    {
        private static readonly ILog Log = LogManager.GetLogger("UserDbRepository");
        private IValidator<User> _validator;
        
        public UserDbRepository()
        {
            Log.Info("Creating UserDbRepository");
            _validator = new UserValidator();
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

        public List<User> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public User FilterByUsername(string username)
        { 
            Log.InfoFormat("Entering FilterByUsername - User");
            
            var con = DbUtils.GetConnection();
            User user = null;
            
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

                        user = new User(username, password);
                        user.Id = id;
                        _validator.Validate(user);
                        
                    }
                }
            }
            Log.InfoFormat("Exiting FilterByRace with value {0}", user);
            return user;
        }
    }
}