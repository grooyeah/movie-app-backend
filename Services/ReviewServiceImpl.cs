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

        public async Task<bool> CreateReviewAsync(ReviewDto review)
        {
            return await _reviewRepository.CreateReviewAsync(review.ToReview());
        }

        public async Task<bool> DeleteReviewAsync(string reviewId)
        {
            return await _reviewRepository.DeleteReviewAsync(reviewId);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            return await _reviewRepository.GetReviewByIdAsync(reviewId);
        }

        public async Task<bool> UpdateReviewAsync(ReviewDto review)
        {
            return await _reviewRepository.UpdateReviewAsync(review.ToReview());
        }
    }
}
