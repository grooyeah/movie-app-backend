using Models;

namespace Interfaces
{
    public interface IReviewService
    {
        IEnumerable<Review> GetAllReviews();
        Review GetReviewById(string reviewId);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(string reviewId);
    }
}
