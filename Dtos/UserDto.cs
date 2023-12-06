using Models;
using System.Text;

namespace Dtos
{
    public class UserDto
{
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Profile Profile { get; set; }

        public UserDto()
        {
        }

        public UserDto(string userId, string username, string password, string email, Profile profile)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            Profile = profile;
        }

        public User ToUser()
        {
            return new User
            {
                UserId = UserId,
                Username = Username,
                PasswordHash = Encoding.ASCII.GetBytes(Password),
                Email = Email
            };
        }
    }
}
