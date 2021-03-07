namespace MotorcycleContest.domain
{
    public class Team : Entity<long>
    {
        public string Name { get; set; }

        public Team(string name)
        {
            Name = name;
        }
    }
}