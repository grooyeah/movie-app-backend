using Dtos;
using Models;

namespace Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<UserDto> UpdateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
