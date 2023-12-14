using Dtos;
using Models;

namespace Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<UserDto> UpdateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
