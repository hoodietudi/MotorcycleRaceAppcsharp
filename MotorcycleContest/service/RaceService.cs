using System.Collections.Generic;
using Domain.model;
using Persistance;

namespace MotorcycleContest.service
{
    public class RaceService
    {
        private readonly RaceDbRepository _repository;

        public RaceService(RaceDbRepository repository)
        {
            _repository = repository;
        }

        public List<Race> GetAllRaces()
        {
            return _repository.FindAll();
        }

        public List<Race> FilterByEngineCapacity(EngineCapacity engineCapacity)
        {
            return _repository.FilterByRequiredEngineCapacity(engineCapacity);
        }
    }
}