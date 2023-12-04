using Models;

namespace Interfaces
{
    public interface IProfileService
{
        Task CreateProfileAsync(Profile profile);
        Task DeleteProfileAsync(string profileId);
        Task<IEnumerable<Profile>> GetAllProfilesAsync();
        Task<Profile> GetProfileByIdAsync(string profileId);
        Task UpdateProfileAsync(Profile profile);
    }
}
