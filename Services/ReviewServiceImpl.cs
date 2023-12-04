using Dtos;
using Interfaces;
using Models;

namespace Services
{
    public class ReviewServiceImpl : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewServiceImpl(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            review.ReviewId = Guid.NewGuid().ToString();
            return await _reviewRepository.CreateReviewAsync(review);
        }

        public async Task<bool> DeleteReviewAsync(string reviewId)
        {
            return await _reviewRepository.DeleteReviewAsync(reviewId);
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            return await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task<ICollection<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }

        public async Task<ICollection<Review>>  GetReviewByProfileIdAsync(string profileId)
        {
            return await _reviewRepository.GetReviewByProfileIdAsync(profileId);
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            return await _reviewRepository.GetReviewByIdAsync(reviewId);
        }

        public async Task<ICollection<Review>> GetReviewsByMovieIdAsync(string imbdId)
        {
            return await _reviewRepository.GetReviewsByMovieIdAsync(imbdId);
        }

        public async Task<ICollection<Review>> GetTopReviewsAsync(string topReviewsCount, string imbdId)
        {
            return await _reviewRepository.GetTopReviewsAsync(topReviewsCount, imbdId);
        }

        public async Task<IEnumerable<string>> GetMostReviewedMoviesAsync(int topMoviesCount)
        {
            return await _reviewRepository.GetMostReviewedMoviesAsync(topMoviesCount);
        }

        public async Task<double> GetAverageRatingForMovieAsync(string imbdId)
        {
            return await _reviewRepository.GetAverageRatingForMovieAsync(imbdId);
        }
    }
}
