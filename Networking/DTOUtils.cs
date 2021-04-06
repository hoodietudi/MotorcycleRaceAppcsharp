using Domain.model;

namespace Networking
{
    public class DTOUtils
    {
        public static User GetFromDto(UserDTO usdto)
        {
            var username = usdto.Username;
            var pass = usdto.Passwd;
            return new User(username, pass);

        }
        public static UserDTO GetDto(User user)
        {
            var id = user.Username;
            var pass = user.Password;
            return new UserDTO(id, pass);
        }
    }
}