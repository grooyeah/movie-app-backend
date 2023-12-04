namespace Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(string userId, string username, string password, string email)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
        }

        public User()
        {
        }
    }
}