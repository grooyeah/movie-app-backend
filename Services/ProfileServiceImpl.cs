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

        public async Task<Profile> GetProfileByUserIdAsync(string userId)
        {
            return await _profileRepository.GetProfileByUserIdAsync(userId);
        }

        public async Task<Profile> UpdateProfileAsync(ProfileDto profile)
        {
            return await _profileRepository.UpdateProfileAsync(profile.ToProfile());
        }

        public async Task<bool> DeleteProfileAsync(string profileId)
        {
            return await _profileRepository.DeleteProfileAsync(profileId);
        }
    }
}
