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

        public void CreateProfile(Profile profile)
        {
            _profileRepository.CreateProfile(profile);
        }

        public void DeleteProfile(string profileId)
        {
            _profileRepository.DeleteProfile(profileId);
        }

        public IEnumerable<Profile> GetAllProfiles()
        {
            return  _profileRepository.GetAllProfiles();
        }

        public Profile GetProfileById(string profileId)
        {
            return _profileRepository.GetProfileById(profileId);
        }

        public void UpdateProfile(Profile profile)
        {
            _profileRepository.UpdateProfile(profile);
        }
    }
}
