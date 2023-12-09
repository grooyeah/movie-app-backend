using Dtos;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Route("/api/")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        [HttpGet("profiles/{userId}")]
        public async Task<IActionResult> GetProfileByUserIdAsync(string userId)
        {
            var profile = await _profileService.GetProfileByUserIdAsync(userId);

            if (profile == null)
            {
                _logger.LogWarning($"Could not retrieve profile with id {userId}. Service returned null.");
                return NotFound();
            }

            return Ok(profile);
        }

        [HttpPost("profiles/{profileId}")]
        public async Task<IActionResult> UpdateProfile([FromBody] Profile profile)
        {
            var profileUpdated = await _profileService.UpdateProfileAsync(profile);

            if (profileUpdated == null)
            {
                _logger.LogWarning($"Could not update profile {profile.UserId}. Service returned null.");
                return NotFound();
            }

            return Ok(profileUpdated);
        }

        [HttpDelete("profiles/{profileId}")]
        public async Task<IActionResult> DeleteProfile( string profileId)
        {
            var result = await _profileService.DeleteProfileAsync(profileId);

            if (!result)
            {
                _logger.LogWarning($"Could not delete profile {profileId}. Service returned null.");
                return NotFound();
            }

            return Ok(result);
        }
    }
}
