using Models;

namespace Interfaces
{
    public interface IProfileRepository
    {
        Task<bool> CreateProfileAsync(Profile profile);
        Task<bool> DeleteProfileAsync(string profileId);
        Task<IEnumerable<Profile>> GetAllProfilesAsync();
        Task<Profile> GetProfileByIdAsync(string profileId);
        Task<bool> UpdateProfileAsync(Profile profile);
    }
}
