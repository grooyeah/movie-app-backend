using Interfaces;
using Models;

namespace Services
{
    public class ProfileServiceImpl : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileServiceImpl(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task CreateProfileAsync(Profile profile)
        {
            await _profileRepository.CreateProfileAsync(profile);
        }

        public async Task DeleteProfileAsync(string profileId)
        {
            await _profileRepository.DeleteProfileAsync(profileId);
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            return await _profileRepository.GetAllProfilesAsync();
        }

        public async Task<Profile> GetProfileByIdAsync(string profileId)
        {
            return await _profileRepository.GetProfileByIdAsync(profileId);
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            await _profileRepository.UpdateProfileAsync(profile);
        }
    }
}
