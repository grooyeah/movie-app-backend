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

        public  void CreateReview(Review review)
        {
            _reviewRepository.CreateReview(review);
        }

        public  void DeleteReview(string reviewId)
        {
            _reviewRepository.DeleteReview(reviewId);
        }

        public  IEnumerable<Review> GetAllReviews()
        {
            return  _reviewRepository.GetAllReviews();
        }

        public Review GetReviewById(string reviewId)
        {
            return _reviewRepository.GetReviewById(reviewId);   
        }

        public  void UpdateReview(Review review)
        {
            _reviewRepository.UpdateReview(review);
        }
    }
}
