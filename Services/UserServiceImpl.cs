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
        
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            return user;
        }

        public  void CreateUser(User user)
        {
           _userRepository.CreateUser(user);
        }

        public  void UpdateUser(User user)
        {
            var userDb = _userRepository.GetUserById(user.UserId);
            
            if (userDb == null)
            {
            }
            
            _userRepository.UpdateUser(user);
           
        }

        public  void DeleteUser(User user)
        {
            _userRepository.DeleteUser(user);
        }
    }
}

