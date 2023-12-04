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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var allExistingUsers = await _dbContext.Users.ToListAsync();
            
            if(!allExistingUsers.Any())
            {
                return null;
            }

            return allExistingUsers;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            
            if(existingUser == null)
            {
                return null;
            }
         
            return existingUser;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var existingUser = await GetUserByEmailAsync(user.Email);

            if(existingUser == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var userToUpdate = user;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            var updatedUser = await GetUserByEmailAsync(userToUpdate.Email);
            
            if(updatedUser == null)
            {
                return false;
            }

            if(updatedUser != userToUpdate)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var userToRemove = user;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            var exisintUser = await GetUserByEmailAsync(user.Email);
            if(exisintUser == null)
            {
                return true;
            }

            return false;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            
            if(existingUser == null)
            {
                return null;
            }

            return existingUser;
        }
    }
}
