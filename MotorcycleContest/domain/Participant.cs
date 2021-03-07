namespace MotorcycleContest.domain
{
    public class Participant
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