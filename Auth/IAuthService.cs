using Models;

namespace Auth
{
    public interface IAuthService
{
        Task<Profile> SignIn(User user);
        Task<User> LogOut(User user);
        Task<Profile> SignUp(Profile profile);
}
}
