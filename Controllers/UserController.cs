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
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, IReviewService reviewService,
        IProfileService profileService, ILogger<UserController> logger)
    {
        _userService = userService;
        _reviewService = reviewService;
        _profileService = profileService;
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
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        await _userService.CreateUserAsync(user);

        var createdUser = await _userService.GetUserByIdAsync(user.UserId);

        if(createdUser == null)
        {
            _logger.LogWarning($"Could not  create user with id {user.UserId}. Service returned null.");
            return NotFound();
        }
        return Ok(createdUser);
    }

    [HttpPut("users/{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] User updatedUser)
    {
        var existingUser = await _userService.GetUserByIdAsync(userId);

        if (existingUser == null)
        {
            _logger.LogWarning($"Could not  create user with id {userId}. Service returned null.");
            return NotFound();
        }

        // Update the existing user properties
        existingUser.Username = updatedUser.Username;
        existingUser.Password = updatedUser.Password;
        existingUser.Email = updatedUser.Email;

        await _userService.UpdateUserAsync(existingUser);

        var updatedUserDb = await _userService.GetUserByIdAsync(userId);

        if(updatedUserDb != updatedUser)
        {
            _logger.LogWarning($"Could not update user with id {userId}. Service returned null.");
            return NoContent();
        }

        return NoContent();
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            _logger.LogWarning($"Could not delete user with id {userId}. Service returned null.");
            return NotFound();
        }

        await _userService.DeleteUserAsync(user);

        var userDeleted = await _userService.GetUserByIdAsync(userId);

        if(userDeleted != null)
        {
            return Ok(user);
        }
        return NoContent();
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
    public async Task<IActionResult> CreateProfile([FromBody] Profile profile)
    {
        await _profileService.CreateProfileAsync(profile);

        var createdProfile = await _profileService.GetProfileByIdAsync(profile.ProfileId);

        if (createdProfile == null)
        {
            _logger.LogWarning($"Could not create profile with id {profile.ProfileId}. Service returned null.");
            return NotFound();
        }

        return Ok(createdProfile);
    }

    [HttpPut("profiles/{profileId}")]
    public async Task<IActionResult> UpdateProfile(string profileId, [FromBody] Profile updatedProfile)
    {
        var existingProfile = await _profileService.GetProfileByIdAsync(profileId);

        if (existingProfile == null)
        {
            _logger.LogWarning($"Could not update profile with id {profileId}. Service returned null.");
            return NotFound();
        }

        // Update the existing profile properties
        existingProfile.Picture = updatedProfile.Picture;
        existingProfile.FavoriteMovies = updatedProfile.FavoriteMovies;

        await _profileService.UpdateProfileAsync(existingProfile);

        var updatedProfileDb = await _profileService.GetProfileByIdAsync(profileId);

        if (updatedProfileDb != updatedProfile)
        {
            _logger.LogWarning($"Could not update profile with id {profileId}. Service returned null.");
            return NoContent();
        }

        return NoContent();
    }

    [HttpDelete("profiles/{profileId}")]
    public async Task<IActionResult> DeleteProfile(string profileId)
    {
        var profile = await _profileService.GetProfileByIdAsync(profileId);

        if (profile == null)
        {
            _logger.LogWarning($"Could not delete profile with id {profileId}. Service returned null.");
            return NotFound();
        }

        await _profileService.DeleteProfileAsync(profileId);

        var profileDeleted = await _profileService.GetProfileByIdAsync(profileId);

        if (profileDeleted != null)
        {
            return Ok(profile);
        }

        return NoContent();
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
    public async Task<IActionResult> CreateReview([FromBody] Review review)
    {
        await _reviewService.CreateReviewAsync(review);

        var createdReview = await _reviewService.GetReviewByIdAsync(review.ReviewId);

        if (createdReview == null)
        {
            _logger.LogWarning($"Could not create review with id {review.ReviewId}. Service returned null.");
            return NotFound();
        }

        return Ok(createdReview);
    }

    [HttpPut("reviews/{reviewId}")]
    public async Task<IActionResult> UpdateReview(string reviewId, [FromBody] Review updatedReview)
    {
        var existingReview = await _reviewService.GetReviewByIdAsync(reviewId);

        if (existingReview == null)
        {
            _logger.LogWarning($"Could not update review with id {reviewId}. Service returned null.");
            return NotFound();
        }

        // Update the existing review properties
        existingReview.ImdbID = updatedReview.ImdbID;
        existingReview.Author = updatedReview.Author;
        existingReview.MovieTitle = updatedReview.MovieTitle;
        existingReview.ReviewTitle = updatedReview.ReviewTitle;
        existingReview.ReviewText = updatedReview.ReviewText;
        existingReview.Rating = updatedReview.Rating;
        existingReview.PublishedOn = updatedReview.PublishedOn;

        await _reviewService.UpdateReviewAsync(existingReview);

        var updatedReviewDb = await _reviewService.GetReviewByIdAsync(reviewId);

        if (updatedReviewDb != updatedReview)
        {
            _logger.LogWarning($"Could not update review with id {reviewId}. Service returned null.");
            return NoContent();
        }

        return NoContent();
    }

    [HttpDelete("reviews/{reviewId}")]
    public async Task<IActionResult> DeleteReview(string reviewId)
    {
        var review = await _reviewService.GetReviewByIdAsync(reviewId);

        if (review == null)
        {
            _logger.LogWarning($"Could not delete review with id {reviewId}. Service returned null.");
            return NotFound();
        }

        await _reviewService.DeleteReviewAsync(reviewId);

        var reviewDeleted = await _reviewService.GetReviewByIdAsync(reviewId);

        if (reviewDeleted != null)
        {
            return Ok(review);
        }

        return NoContent();
    }

    #endregion


    #region Login & Sign up

    [HttpPost("auth/login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        // Validate user credentials and generate a token
        // For simplicity, let's assume you have an authentication service
        

        return Ok();
    }

    [HttpPost("auth/signup")]
    public async Task<IActionResult> SignUp([FromBody] Profile profile)
    {
        return Ok(profile);
    }

    [HttpPost("auth/logout")]
    public async Task<IActionResult> LogOut([FromQuery] string userId)
    {
        

        return Ok("Logged out successfully.");
    }

    #endregion
}

