using Domain.model;

namespace Persistance.interfaces
{
    public interface IUserRepository : IRepository<int, User>
    {
        User FilterByUsername(string username);
    }
}