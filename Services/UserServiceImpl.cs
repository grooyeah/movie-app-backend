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

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User> UpdateUserAsync(UserDto user)
        {
            var userDb = await _userRepository.GetUserByIdAsync(user.ToUser().UserId);

            if (userDb == null)
            {
                return null;
            }

            var result = await _userRepository.UpdateUserAsync(user.ToUser());

            return result;
        }

        public async Task<bool> DeleteUserAsync(UserDto user)
        {
           var result = await _userRepository.DeleteUserAsync(user.ToUser());
            return result;
        }
    }
}

