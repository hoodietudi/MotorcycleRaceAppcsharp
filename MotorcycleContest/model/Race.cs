using System.Collections.Generic;

namespace MotorcycleContest.model
{
    public class Race : Entity<int>
    {
       public EngineCapacity RequiredEngineCapacity { get; set; }
        
       public string Name { get; set; }

       public Race(EngineCapacity requiredEngineCapacity, string name)
       {
           RequiredEngineCapacity = requiredEngineCapacity;
           Name = name;
       }
    }
}