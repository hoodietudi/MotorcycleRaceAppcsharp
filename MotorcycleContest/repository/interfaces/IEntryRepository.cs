using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
{
    public interface IEntryRepository : IRepository<int, Entry>
    {
        List<Entry> FilterByRace(Race race);
    }
}