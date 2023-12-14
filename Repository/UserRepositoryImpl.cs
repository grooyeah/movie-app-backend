using Database;
using Dtos;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Security.Cryptography;
using System.Text;

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

        public async Task<UserDto> UpdateUserAsync(UserDto user)
        {
            var userToUpdate = await GetUserByIdAsync(user.UserId);
            
            if(userToUpdate == null)
            {
                return null;
            }

            userToUpdate.Email = user.Email;
            userToUpdate.Username = user.Username;

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userToUpdate.PasswordHash = passwordHash;
            userToUpdate.PasswordSalt = passwordSalt;
            
            await _dbContext.SaveChangesAsync();

            return userToUpdate.ToUserDto();
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var existingUser = await GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                return false;
            }
            var existingUserProfile = await _dbContext.Profiles.FirstOrDefaultAsync(x => x.UserId == userId);
            var existingUserReviews = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.RProfileId == existingUserProfile.ProfileId);
            var existingUserMovieList = await _dbContext.MovieLists.FirstOrDefaultAsync(x => x.MProfileId == existingUserProfile.ProfileId);

            _dbContext.Users.Remove(existingUser);
            _dbContext.Profiles.Remove(existingUserProfile);
            _dbContext.Reviews.Remove(existingUserReviews);
            _dbContext.MovieLists.Remove(existingUserMovieList);

            await _dbContext.SaveChangesAsync();

            var exisintUser = await GetUserByIdAsync(userId);

            if(exisintUser == null)
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
