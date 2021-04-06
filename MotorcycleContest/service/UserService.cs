using Domain.model;
using Persistance;

namespace MotorcycleContest.service
{
    public class UserService
    {
        private readonly UserDbRepository _repository;

        public UserService(UserDbRepository repository)
        {
            _repository = repository;
        }

        public User GetUser(string username)
        {
            return _repository.FilterByUsername(username);
        }
    }
}