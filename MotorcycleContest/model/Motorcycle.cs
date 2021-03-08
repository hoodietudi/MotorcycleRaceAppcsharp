namespace MotorcycleContest.model
{
    public class Motorcycle : Entity<long>
    {
        public EngineCapacity EngineCapacity { get; set; }

        public Motorcycle(EngineCapacity engineCapacity)
        {
            EngineCapacity = engineCapacity;
        }
    }
}