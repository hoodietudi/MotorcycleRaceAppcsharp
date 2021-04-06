using Domain.model;
using Persistance;

namespace MotorcycleContest.service
{
    public class MotorcycleService
    {
        private readonly MotorcycleDbRepository _repository;

        public MotorcycleService(MotorcycleDbRepository repository)
        {
            _repository = repository;
        }
        
        public Motorcycle AddMotorcycle(EngineCapacity engineCapacity) {
            var motorcycle = new Motorcycle(engineCapacity);
            _repository.Save(motorcycle);
            motorcycle = _repository.LastMotorcycleAdded();
            return motorcycle;
        }
    }
}