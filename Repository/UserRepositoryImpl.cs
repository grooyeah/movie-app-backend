using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class UserRepositoryImpl : IUserRepository
{
        private readonly MovieAppDbContext _dbContext;

        public UserRepositoryImpl(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<User> UpdateUserAsync(User user)
        {
            var userToUpdate = user;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            var updatedUser = await GetUserByIdAsync(userToUpdate.UserId);
            
            if(updatedUser == null)
            {
                return null;
            }

            if(updatedUser != userToUpdate)
            {
                return null;
            }

            return updatedUser;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var userToRemove = user;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            var exisintUser = await GetUserByIdAsync(user.UserId);
            if(exisintUser == null)
            {
                return true;
            }

            return false;
        }
    }
}
