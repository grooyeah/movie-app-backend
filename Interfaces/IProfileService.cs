using Dtos;
using Models;

namespace Interfaces
{
    public interface IProfileService
{
        Task<bool> CreateProfileAsync(ProfileDto profile);
        Task<bool> DeleteProfileAsync(string profileId);
        Task<IEnumerable<Profile>> GetAllProfilesAsync();
        Task<Profile> GetProfileByIdAsync(string profileId);
        Task<bool> UpdateProfileAsync(ProfileDto profile);
    }
}
