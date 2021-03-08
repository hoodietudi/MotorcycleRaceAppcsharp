using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
{
    public interface IUserRepository : IRepository<long, User>
    {
        List<User> FilterByUsername(string username);
    }
}