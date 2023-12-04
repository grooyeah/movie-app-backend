using Dtos;
using Models;

namespace Auth
{
    public interface IAuthService
{
        Task<string> SignIn(string email, string password);
        Task<User> LogOut(UserDto user);
        Task<Profile> SignUp(ProfileDto profile);
}
}
