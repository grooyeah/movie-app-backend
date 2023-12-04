using Dtos;
using Models;

namespace Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        Task<bool> CreateUserAsync(UserDto user);
        Task<bool> UpdateUserAsync(UserDto user);
        Task<bool> DeleteUserAsync(UserDto user);
        Task<User> GetUserByEmailAsync(string email);

    }
}
