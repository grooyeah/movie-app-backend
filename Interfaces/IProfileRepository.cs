using Models;

namespace Interfaces
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetAllProfiles();
        Profile GetProfileById(string profileId);
        void CreateProfile(Profile profile);
        void UpdateProfile(Profile profile);
        void DeleteProfile(string profileId);
    }
}
