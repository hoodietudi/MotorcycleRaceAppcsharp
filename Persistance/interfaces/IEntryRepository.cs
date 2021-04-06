using System.Collections.Generic;
using Domain.model;

namespace Persistance.interfaces
{
    public interface IEntryRepository : IRepository<int, Entry>
    {
        List<Entry> FilterByRace(Race race);
    }
}