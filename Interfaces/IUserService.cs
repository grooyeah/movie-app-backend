using Dtos;
using Models;

namespace Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<User> UpdateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(UserDto user);
    }
}
