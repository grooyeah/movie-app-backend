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

        public async Task CreateProfileAsync(Profile profile)
        {
            await _dbContext.AddAsync(profile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProfileAsync(string profileId)
        {
            var profile = await GetProfileByIdAsync(profileId);
            if (profile != null)
            {
                _dbContext.Remove(profile);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            return await _dbContext.Profiles.ToListAsync();
        }

        public async Task<Profile> GetProfileByIdAsync(string profileId)
        {
            return await _dbContext.Profiles.SingleOrDefaultAsync(x => x.ProfileId == profileId);
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            _dbContext.Profiles.Update(profile);
            await _dbContext.SaveChangesAsync();
        }

    }
}
