using Models;

namespace Interfaces
{
    public interface IProfileRepository
    {
        Task<bool> CreateProfileAsync(Profile profile);
        Task<bool> DeleteProfileAsync(string userId);
        Task<Profile> GetProfileByUserIdAsync(string userId);
        Task<Profile> UpdateProfileAsync(Profile profile);
    }
}
