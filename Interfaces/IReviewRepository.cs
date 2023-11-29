using Models;

namespace Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAllReviews();
        Review GetReviewById(string reviewId);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(string reviewId);
    }
}
