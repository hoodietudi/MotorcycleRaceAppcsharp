using System;

namespace Domain.model
{
    [Serializable]
    public class Participant : Entity<int>
    {
        public string Name { get; set; }
        public Motorcycle Motorcycle { get; set; }
        public Team Team { get; set; }

        public Participant(string name, Motorcycle motorcycle, Team team)
        {
            Name = name;
            Motorcycle = motorcycle;
            Team = team;
        }
    }
}