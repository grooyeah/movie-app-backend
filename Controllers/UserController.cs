using Interfaces;
using Microsoft.AspNetCore.Components;
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
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();

        if(users == null)
        {
            _logger.LogWarning("Could not retrieve all users. Service returned null.");
            return NotFound();
        }
        return Ok(users);
    }

    [HttpGet("users/{userId}")]
    public IActionResult GetUserById(string userId)
    {
        var user = _userService.GetUserById(userId);

        if (user == null)
        {
            _logger.LogWarning($"Could not retrieve user with id {userId}. Service returned null.");
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("users")]
    public IActionResult CreateUser([FromBody] User user)
    {
        _userService.CreateUser(user);

        var createdUser = _userService.GetUserById(user.UserId);

        if(createdUser == null)
        {
            _logger.LogWarning($"Could not  create user with id {user.UserId}. Service returned null.");
            return NotFound();
        }
        return Ok(createdUser);
    }

    [HttpPut("users/{userId}")]
    public IActionResult UpdateUser(string userId, [FromBody] User updatedUser)
    {
        var existingUser = _userService.GetUserById(userId);

        if (existingUser == null)
        {
            _logger.LogWarning($"Could not  create user with id {userId}. Service returned null.");
            return NotFound();
        }

        // Update the existing user properties
        existingUser.Username = updatedUser.Username;
        existingUser.Password = updatedUser.Password;
        existingUser.Email = updatedUser.Email;

        _userService.UpdateUser(existingUser);

        var updatedUserDb = _userService.GetUserById(userId);

        if(updatedUserDb != updatedUser)
        {
            _logger.LogWarning($"Could not update user with id {userId}. Service returned null.");
            return NoContent();
        }

        return NoContent();
    }

    [HttpDelete("users/{userId}")]
    public IActionResult DeleteUser(string userId)
    {
        var user = _userService.GetUserById(userId);

        if (user == null)
        {
            _logger.LogWarning($"Could not delete user with id {userId}. Service returned null.");
            return NotFound();
        }

        _userService.DeleteUser(user);

        var userDeleted = _userService.GetUserById(userId);

        if(userDeleted != null)
        {
            return Ok(user);
        }
        return NoContent();
    }
    #endregion

    #region Profile

    [HttpGet("profiles/{profileId}")]
    public IActionResult GetProfile(string profileId)
    {
        var profile = _profileService.GetProfileById(profileId);

        if (profile == null)
        {
            _logger.LogWarning($"Could not retrieve profile with id {profileId}. Service returned null.");
            return NotFound();
        }

        return Ok(profile);
    }

    [HttpPost("profiles")]
    public IActionResult CreateProfile([FromBody] Profile profile)
    {
        _profileService.CreateProfile(profile);

        var createdProfile = _profileService.GetProfileById(profile.ProfileId);

        if (createdProfile == null)
        {
            _logger.LogWarning($"Could not create profile with id {profile.ProfileId}. Service returned null.");
            return NotFound();
        }

        return Ok(createdProfile);
    }

    [HttpPut("profiles/{profileId}")]
    public IActionResult UpdateProfile(string profileId, [FromBody] Profile updatedProfile)
    {
        var existingProfile = _profileService.GetProfileById(profileId);

        if (existingProfile == null)
        {
            _logger.LogWarning($"Could not update profile with id {profileId}. Service returned null.");
            return NotFound();
        }

        // Update the existing profile properties
        existingProfile.Picture = updatedProfile.Picture;
        existingProfile.FavoriteMovies = updatedProfile.FavoriteMovies;

        _profileService.UpdateProfile(existingProfile);

        var updatedProfileDb = _profileService.GetProfileById(profileId);

        if (updatedProfileDb != updatedProfile)
        {
            _logger.LogWarning($"Could not update profile with id {profileId}. Service returned null.");
            return NoContent();
        }

        return NoContent();
    }

    [HttpDelete("profiles/{profileId}")]
    public IActionResult DeleteProfile(string profileId)
    {
        var profile = _profileService.GetProfileById(profileId);

        if (profile == null)
        {
            _logger.LogWarning($"Could not delete profile with id {profileId}. Service returned null.");
            return NotFound();
        }

        _profileService.DeleteProfile(profileId);

        var profileDeleted = _profileService.GetProfileById(profileId);

        if (profileDeleted != null)
        {
            return Ok(profile);
        }

        return NoContent();
    }

    #endregion

    #region Review

    [HttpGet("reviews/{reviewId}")]
    public IActionResult GetReview(string reviewId)
    {
        var review = _reviewService.GetReviewById(reviewId);

        if (review == null)
        {
            _logger.LogWarning($"Could not retrieve review with id {reviewId}. Service returned null.");
            return NotFound();
        }

        return Ok(review);
    }

    [HttpPost("reviews")]
    public IActionResult CreateReview([FromBody] Review review)
    {
        _reviewService.CreateReview(review);

        var createdReview = _reviewService.GetReviewById(review.ReviewId);

        if (createdReview == null)
        {
            _logger.LogWarning($"Could not create review with id {review.ReviewId}. Service returned null.");
            return NotFound();
        }

        return Ok(createdReview);
    }

    [HttpPut("reviews/{reviewId}")]
    public IActionResult UpdateReview(string reviewId, [FromBody] Review updatedReview)
    {
        var existingReview = _reviewService.GetReviewById(reviewId);

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

        _reviewService.UpdateReview(existingReview);

        var updatedReviewDb = _reviewService.GetReviewById(reviewId);

        if (updatedReviewDb != updatedReview)
        {
            _logger.LogWarning($"Could not update review with id {reviewId}. Service returned null.");
            return NoContent();
        }

        return NoContent();
    }

    [HttpDelete("reviews/{reviewId}")]
    public IActionResult DeleteReview(string reviewId)
    {
        var review = _reviewService.GetReviewById(reviewId);

        if (review == null)
        {
            _logger.LogWarning($"Could not delete review with id {reviewId}. Service returned null.");
            return NotFound();
        }

        _reviewService.DeleteReview(reviewId);

        var reviewDeleted = _reviewService.GetReviewById(reviewId);

        if (reviewDeleted != null)
        {
            return Ok(review);
        }

        return NoContent();
    }

    #endregion


    #region Login & Sing up

    [HttpGet("auth/login/{user}")]
    public IActionResult Login([FromBody] User user)
    {
        //Authentication service
        return Ok();
    }

    [HttpGet("auth/signup/{profile}")]
    public IActionResult SignUp([FromBody] Profile profile)
    {
        //Authentication Service
        return Ok();
    }

    [HttpGet("auth/logout/{userid}")]
    public IActionResult LogOut(string userId)
    {
        return Ok();
    }

    #endregion
}

