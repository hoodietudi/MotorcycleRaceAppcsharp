namespace MotorcycleContest.model
{
    public class Motorcycle : Entity<int>
    {
        public EngineCapacity EngineCapacity { get; set; }

        public Motorcycle(EngineCapacity engineCapacity)
        {
            EngineCapacity = engineCapacity;
        }
    }
}