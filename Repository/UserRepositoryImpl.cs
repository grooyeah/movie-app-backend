using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class UserRepositoryImpl : IUserRepository
{
        private readonly UserDbContext _dbContext;

        public UserRepositoryImpl(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(string userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public  void UpdateUser(User user)
        {
            // Assuming user is attached to the DbContext
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async void DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
