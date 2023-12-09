using Models;

namespace Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
