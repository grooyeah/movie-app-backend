using Dtos;

namespace Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public User(string userId, string username, byte[] passwordHash, byte[] passwordSalt, string email)
        {
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Email = email;
        }

        public User()
        {
        }

        public UserDto ToUserDto()
        {
            return new UserDto
            {
                UserId = UserId,
                Username = Username,
                Email = Email
            };
        }
    }
}