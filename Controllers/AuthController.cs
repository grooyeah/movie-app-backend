using Auth;
using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Route("/api/")]
    [ApiController]
    public class AuthController : ControllerBase
{
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var existingUser = await _authService.Login(loginModel.Username, loginModel.Password);

            if (existingUser == null)
            {
                _logger.LogWarning($"Could not find user with username {loginModel.Username}");
                return NotFound("User could not be found.");
            }

            return Ok(existingUser);
        }

        [HttpPost("auth/logout/{userId}")]
        public async Task<IActionResult> Logout([FromQuery] string userId)
        {
            var result = await _authService.LogOut(userId);

            if (!result)
            {
                _logger.LogWarning($"Could log out user. user not found");
                return NoContent();
            }

            return Ok("Logged out successfully.");
        }
    }
}
