using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class ProfileRepositoryImpl : IProfileRepository
    {
        private readonly UserDbContext _dbContext;

        public ProfileRepositoryImpl(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProfileAsync(Profile profile)
        {
            var profileToAdd = profile;

            await _dbContext.AddAsync(profile);
            await _dbContext.SaveChangesAsync();

            var existingProfile = GetProfileByIdAsync(profile.ProfileId);

            if(existingProfile == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProfileAsync(string profileId)
        {
            var profile = await GetProfileByIdAsync(profileId);

            if (profile == null)
            {
                return false;
            }
            
            _dbContext.Remove(profile);
            await _dbContext.SaveChangesAsync();

            var existingProfile = await GetProfileByIdAsync(profile.ProfileId);
            
            if(existingProfile == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            var allExistingProfiles = await _dbContext.Profiles.ToListAsync();

            if(!allExistingProfiles.Any())
            {
                return null;
            }

            return allExistingProfiles;
        }

        public async Task<Profile> GetProfileByIdAsync(string profileId)
        {
            var existingProfile = await _dbContext.Profiles.SingleOrDefaultAsync(x => x.ProfileId == profileId);

            if(existingProfile == null)
            {
                return null;
            }

            return existingProfile;
        }

        public async Task<bool> UpdateProfileAsync(Profile profile)
        {
            var profileToUpdate = profile;

            _dbContext.Profiles.Update(profile);
            await _dbContext.SaveChangesAsync();

            var updatedProfile = await GetProfileByIdAsync(profileToUpdate.ProfileId);
            
            if(updatedProfile == null)
            {
                return false;
            }

            if(updatedProfile != profileToUpdate)
            {
                return false;
            }

            return true;
        }
    }
}
