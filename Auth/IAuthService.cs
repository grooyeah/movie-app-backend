using Dtos;
using Models;

namespace Auth
{
    public interface IAuthService
{
        Task<string> Login(string email, string password);
        Task<bool> LogOut(string userId);
        Task<UserDto> SignUp(SignUpModel signUpModel);
}
}
