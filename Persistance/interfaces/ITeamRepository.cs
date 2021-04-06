using Domain.model;

namespace Persistance.interfaces
{
    public interface ITeamRepository : IRepository<int, Team>
    {
        Team FilterByName(string name);
    }
}