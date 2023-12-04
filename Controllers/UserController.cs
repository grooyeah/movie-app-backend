using Auth;
using Dtos;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace movie_app_backend.Controllers;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IReviewService _reviewService;
    private readonly IProfileService _profileService;
    private readonly IAuthService _authService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, IReviewService reviewService,
        IProfileService profileService,IAuthService authService,
        ILogger<UserController> logger)
    {
        _userService = userService;
        _reviewService = reviewService;
        _profileService = profileService;
        _authService = authService;
        _logger = logger;
    }

    #region User 

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();

        if(users == null)
        {
            _logger.LogWarning("Could not retrieve all users. Service returned null.");
            return NotFound();
        }
        return Ok(users);
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            _logger.LogWarning($"Could not retrieve user with id {userId}. Service returned null.");
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto user)
    {
        var result = await _userService.CreateUserAsync(user);

        if(!result)
        {
            _logger.LogWarning($"Could not  create user with id {user.ToUser().UserId}. Service returned null.");
            return NotFound();
        }
        return Ok("User created successfully!");
    }

    [HttpPut("users/{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDto userToUpdate)
    {
        var result = await _userService.UpdateUserAsync(userToUpdate);

        if(!result)
        {
            _logger.LogWarning($"Could not update user {userToUpdate.Username}. Service returned null.");
            return NotFound();
        }

        return Ok("User updated successfully!");
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(UserDto user)
    {
        var result = await _userService.DeleteUserAsync(user);

        if (!result)
        {
            _logger.LogWarning($"Could not delete user {user.Username}. Service returned null.");
            return NotFound();
        }

        return Ok("User deleted successfully!");
    }
    #endregion

    #region Profile

    [HttpGet("profiles/{profileId}")]
    public async Task<IActionResult> GetProfile(string profileId)
    {
        var profile = await _profileService.GetProfileByIdAsync(profileId);

        if (profile == null)
        {
            _logger.LogWarning($"Could not retrieve profile with id {profileId}. Service returned null.");
            return NotFound();
        }

        return Ok(profile);
    }

    [HttpPost("profiles")]
    public async Task<IActionResult> CreateProfile([FromBody] ProfileDto profile)
    {
        var result = await _profileService.CreateProfileAsync(profile);

        if (!result)
        {
            _logger.LogWarning($"Could not create profile {profile.User.Username}. Service returned null.");
            return NotFound();
        }

        return Ok("Profile created successfully!");
    }

    [HttpPut("profiles/{profileId}")]
    public async Task<IActionResult> UpdateProfile(string profileId, [FromBody] ProfileDto profileToUpdate)
    {
        var result = await _profileService.UpdateProfileAsync(profileToUpdate);

        if (!result)
        {
            _logger.LogWarning($"Could not update profile {profileToUpdate.User.Username}. Service returned null.");
            return NotFound();
        }

        return Ok("Profile updated successfully!");
    }

    [HttpDelete("profiles/{profileId}")]
    public async Task<IActionResult> DeleteProfile(string profileId)
    {
        var result = await _profileService.DeleteProfileAsync(profileId);

        if (!result)
        {
            _logger.LogWarning($"Could not delete profile {profileId}. Service returned null.");
            return NotFound();
        }

        return Ok("Profile deleted successfully!");
    }

    #endregion

    #region Review

    [HttpGet("reviews/{reviewId}")]
    public async Task<IActionResult> GetReview(string reviewId)
    {
        var review = await _reviewService.GetReviewByIdAsync(reviewId);

        if (review == null)
        {
            _logger.LogWarning($"Could not retrieve review with id {reviewId}. Service returned null.");
            return NotFound();
        }

        return Ok(review);
    }

    [HttpPost("reviews")]
    public async Task<IActionResult> CreateReview([FromBody] ReviewDto review)
    {
        var result = await _reviewService.CreateReviewAsync(review);

        if (!result)
        {
            _logger.LogWarning($"Could not create review . Service returned null.");
            return NotFound();
        }

        return Ok("Review created successfully!");
    }

    [HttpPut("reviews/{reviewId}")]
    public async Task<IActionResult> UpdateReview(string reviewId, [FromBody] ReviewDto reviewToUpdate)
    {
        var result = await _reviewService.UpdateReviewAsync(reviewToUpdate);

        if (!result)
        {
            _logger.LogWarning($"Could not update review . Service returned null.");
            return NotFound();
        }

        return Ok("Review updated successfully!");
    }

    [HttpDelete("reviews/{reviewId}")]
    public async Task<IActionResult> DeleteReview(string reviewId)
    {
        var result = await _reviewService.DeleteReviewAsync(reviewId);

        if (!result)
        {
            _logger.LogWarning($"Could not delete review . Service returned null.");
            return NotFound();
        }

        return Ok("Review deleted successfully!");
    }

    #endregion


    #region Login & Sign up

    [HttpPost("auth/login")]
    public async Task<IActionResult> SignIn(UserDto userDto)
    {
       var token = await _authService.SignIn(userDto.Email, userDto.Password);
       if(token == null)
        {
            _logger.LogWarning($"Could not find user with email {userDto.Email}");
            return NotFound("User could not be found.");
        }
        return Ok(token);
    }

    [HttpPost("auth/signup")]
    public async Task<IActionResult> SignUp([FromBody] ProfileDto profile)
    { 
        var createdUser = await _authService.SignUp(profile);
        if (createdUser == null)
        {
            _logger.LogWarning($"Could not create user.");
            return NotFound("User could not be found.");
        }
        return Ok(createdUser);
    }

    [HttpPost("auth/logout")]
    public async Task<IActionResult> LogOut([FromBody] UserDto user)
    {
        var userToLogout = await _authService.LogOut(user);
        if(user == null)
        {
            _logger.LogWarning($"Could log out user. user not found");
            return NoContent();
        }

        return Ok("Logged out successfully.");
    }

    #endregion
}

