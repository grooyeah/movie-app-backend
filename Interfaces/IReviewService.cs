using Dtos;
using Models;

namespace Interfaces
{
    public interface IReviewService
    {
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(string reviewId);
        Task<ICollection<Review>> GetAllReviewsAsync();
        Task<ICollection<Review>>  GetReviewByProfileIdAsync(string profileId);
        Task<ICollection<Review>> GetReviewsByMovieIdAsync(string imbdId);
        Task<ICollection<Review>> GetTopReviewsAsync(string topReviewsCount,string imbdId);
        Task<IEnumerable<string>> GetMostReviewedMoviesAsync(int topMoviesCount);
        Task<Review> GetReviewByIdAsync(string reviewId);
        Task<double> GetAverageRatingForMovieAsync(string imbdId);
    }
}
