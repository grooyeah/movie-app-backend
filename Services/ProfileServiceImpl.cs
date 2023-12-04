using Dtos;
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

        public async Task<bool> CreateProfileAsync(ProfileDto profile)
        {
            return await _profileRepository.CreateProfileAsync(profile.ToProfile());
        }

        public async Task<bool> DeleteProfileAsync(string profileId)
        {
            return await _profileRepository.DeleteProfileAsync(profileId);
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            return await _profileRepository.GetAllProfilesAsync();
        }

        public async Task<Profile> GetProfileByIdAsync(string profileId)
        {
            return await _profileRepository.GetProfileByIdAsync(profileId);
        }

        public async Task<bool> UpdateProfileAsync(ProfileDto profile)
        {
            return await _profileRepository.UpdateProfileAsync(profile.ToProfile());
        }
    }
}
