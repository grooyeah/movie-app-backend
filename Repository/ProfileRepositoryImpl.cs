using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class ProfileRepositoryImpl : IProfileRepository
    {
        private readonly MovieAppDbContext _dbContext;

        public ProfileRepositoryImpl(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateProfileAsync(Profile profile)
        {
            var profileToAdd = profile;

            await _dbContext.AddAsync(profile);
            await _dbContext.SaveChangesAsync();

            var existingProfile = GetProfileByUserIdAsync(profile.UserId);

            if (existingProfile == null)
            {
                return false;
            }

            return true;
        }

        public async Task<Profile> GetProfileByUserIdAsync(string userId)
        {
            var existingProfile = await _dbContext.Profiles.SingleOrDefaultAsync(x => x.UserId == userId);

            if (existingProfile == null)
            {
                return null;
            }

            return existingProfile;
        }

        public async Task<Profile> UpdateProfileAsync(Profile profile)
        {
            var profileToUpdate = profile;

            _dbContext.Profiles.Update(profile);
            await _dbContext.SaveChangesAsync();

            var updatedProfile = await GetProfileByUserIdAsync(profileToUpdate.UserId);
            
            if(updatedProfile == null)
            {
                return null;
            }

            if(updatedProfile != profileToUpdate)
            {
                return null;
            }

            return updatedProfile;
        }

        public async Task<bool> DeleteProfileAsync(string userId)
        {
            var profile = await GetProfileByUserIdAsync(userId);

            if (profile == null)
            {
                return false;
            }

            _dbContext.Remove(profile);
            await _dbContext.SaveChangesAsync();

            var existingProfile = await GetProfileByUserIdAsync(profile.UserId);

            if (existingProfile == null)
            {
                return false;
            }

            return true;
        }
    }
}
