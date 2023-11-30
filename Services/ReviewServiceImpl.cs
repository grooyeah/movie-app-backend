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

        public async Task CreateReviewAsync(Review review)
        {
            await _reviewRepository.CreateReviewAsync(review);
        }

        public async Task DeleteReviewAsync(string reviewId)
        {
            await _reviewRepository.DeleteReviewAsync(reviewId);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            return await _reviewRepository.GetReviewByIdAsync(reviewId);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewRepository.UpdateReviewAsync(review);
        }
    }
}
