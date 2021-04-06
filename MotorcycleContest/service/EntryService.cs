using Domain.model;
using Persistance;

namespace MotorcycleContest.service
{
    public class EntryService
    {
        private readonly EntryDbRepository _repository;

        public EntryService(EntryDbRepository repository)
        {
            _repository = repository;
        }

        public int ParticipantsAtRace(Race race)
        {
            return _repository.FilterByRace(race).Count;
        }

        public void AddEntry(Entry entry)
        {
            _repository.Save(entry);
        }
    }
}