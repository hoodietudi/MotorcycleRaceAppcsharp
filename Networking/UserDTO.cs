using System;

namespace Networking
{
    [Serializable]
    public class UserDTO
    {
        public virtual string Username { get; }
        
        public virtual string Passwd { get; }

        public UserDTO(string username, string passwd = "")
        {
            this.Username = username;
            Passwd = passwd;
        }
    }
}