using System.Collections.Generic;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class UserDbRepository : AbstractRepository<long, User>, IUserRepository
    {
        public UserDbRepository(IValidator<User> validator) : base(validator)
        {
        }

        public List<User> FilterByUsername(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}