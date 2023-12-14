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
            var existingProfile = await _dbContext.Profiles.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId);

            if (existingProfile == null)
            {
                return null;
            }

            var existingUserReviews = await _dbContext.Reviews
                .AsNoTracking()
                .Where(review => review.RProfileId == existingProfile.ProfileId)
                .ToListAsync();

            var existingUserMovieLists = await _dbContext.MovieLists
                .AsNoTracking()
                .Where(list => list.MProfileId == existingProfile.ProfileId)
                .ToListAsync();

            existingProfile.Reviews = existingUserReviews;
            existingProfile.MovieLists = existingUserMovieLists;

            _dbContext.Profiles.Entry(existingProfile).State = EntityState.Detached;

            return existingProfile;
        }

        public async Task<Profile> UpdateProfileAsync(Profile profile)
        {
            var profileToUpdate = await GetProfileByUserIdAsync(profile.UserId);
            
            if(profileToUpdate == null)
            {
                return null;
            }

            profileToUpdate.Picture = profile.Picture;
            profileToUpdate.MovieLists = profile.MovieLists;
            profileToUpdate.Reviews = profile.Reviews;
            
            await _dbContext.SaveChangesAsync();

            return profileToUpdate;
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
