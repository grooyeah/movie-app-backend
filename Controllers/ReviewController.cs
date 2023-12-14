using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace movie_app_backend.Controllers;

[Route("/api/")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost("reviews")]
    public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(errors);
        }

        var createdReview = await _reviewService.CreateReviewAsync(review);
        return Created($"/Review/{createdReview.ReviewId}", createdReview);
    }

    [HttpPut("reviews")]
    public async Task<ActionResult<Review>> UpdateReview([FromBody] Review review)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedReview = await _reviewService.UpdateReviewAsync(review);

        return Ok(updatedReview);
    }
    
    [HttpDelete("reviews/{reviewId}")]
    public async Task<IActionResult> RemoveReview(string reviewId)
    {
        var isDeleted = await _reviewService.DeleteReviewAsync(reviewId);
        if (!isDeleted)
        {
            return NotFound();
        }

        return Ok("Review " + reviewId + " deleted");
    }
    
    [HttpGet("reviews")]
    public async Task<ActionResult<ICollection<Review>>>GetAllReviews()
    {
        var reviews = await _reviewService.GetAllReviewsAsync();
        return Ok(reviews);
    }

    [HttpGet("reviews/profile/{profileId}")]
    public async Task<ActionResult<Review>> GetReviewByProfileId(string profileId)
    {
        var review = await _reviewService.GetReviewByProfileIdAsync(profileId);
        return Ok(review);
    }

    [HttpGet("reviews/{reviewId}")]
    public async Task<ActionResult<Review>> GetReviewById(string reviewId)
    {
        var review = await _reviewService.GetReviewByIdAsync(reviewId);
        return Ok(review);
    }

    [HttpGet("reviews/movie/{imdbId}")]
    public async Task<ActionResult<ICollection<Review>>> GetReviewsByMovieId(string imdbId)
    {
        var reviews = await _reviewService.GetReviewsByMovieIdAsync(imdbId);
        return Ok(reviews);
    }

    [HttpGet("reviews/top/{topReviewsCount}/movie/{imdbId}")]
    public async Task<ActionResult<ICollection<Review>>> GetTopReviews(string topReviewsCount, string imdbId)
    {
        var reviews = await _reviewService.GetTopReviewsAsync(topReviewsCount, imdbId);
        return Ok(reviews);
    }

    [HttpGet("reviews/most-reviewed/{topMoviesCount:int}")]
    public async Task<ActionResult<ICollection<Review>>> GetMostReviewedMovies(int topMoviesCount)
    {
        var movies = await _reviewService.GetMostReviewedMoviesAsync(topMoviesCount);
        return Ok(movies);
    }

    [HttpGet("reviews/average-rating/movie/{imdbId}")]
    public async Task<ActionResult<double>> GetAverageRatingForMovie(string imdbId)
    {
        var averageRating = await _reviewService.GetAverageRatingForMovieAsync(imdbId);
        return Ok(averageRating);
    }
}