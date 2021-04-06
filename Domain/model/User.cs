namespace Domain.model
{
    public class User : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return Username + " " + Password;
        }
    }
}