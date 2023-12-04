using Dtos;
using Interfaces;
using Models;

namespace Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServiceImpl(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<bool> CreateUserAsync(UserDto user)
        {
            user.ToUser().UserId = Guid.NewGuid().ToString();
            var result = await _userRepository.CreateUserAsync(user.ToUser());
            return result;
        }

        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            var userDb = await _userRepository.GetUserByIdAsync(user.ToUser().UserId);

            if (userDb == null)
            {
                return false;
            }
            var result = await _userRepository.UpdateUserAsync(user.ToUser());
            return result;
        }

        public async Task<bool> DeleteUserAsync(UserDto user)
        {
           var result = await _userRepository.DeleteUserAsync(user.ToUser());
            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            return existingUser;
        }
    }
}

