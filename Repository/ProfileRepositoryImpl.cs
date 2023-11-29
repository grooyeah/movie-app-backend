using Database;
using Interfaces;
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

        public void CreateProfile(Profile profile)
        {
            _dbContext.Add(profile);
        }

        public void DeleteProfile(string profileId)
        {
            _dbContext.Remove(profileId);
        }

        public IEnumerable<Profile> GetAllProfiles()
        {
            return _dbContext.Profiles.AsEnumerable();
        }

        public Profile GetProfileById(string profileId)
        {
            return _dbContext.Profiles.SingleOrDefault(x => x.ProfileId == profileId);
        }

        public  void UpdateProfile(Profile profile)
        {
            _dbContext.Profiles.Update(profile);
        }

    }
}
