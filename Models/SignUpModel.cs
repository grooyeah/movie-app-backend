namespace Models
{
    public class SignUpModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public SignUpModel()
        {
        }

        public SignUpModel(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }

    }
}
