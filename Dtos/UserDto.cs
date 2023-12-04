using Models;

namespace Dtos
{
    public class UserDto
{
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserDto(string userId, string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public UserDto()
        {
        }

        public User ToUser()
        {
            return new User
            {
                Username = Username,
                Email = Email
            };
        }
    }
}
