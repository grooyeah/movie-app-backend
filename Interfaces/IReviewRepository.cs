using Models;

namespace Interfaces
{
    public interface IReviewRepository
    {
        Task<bool> CreateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(string reviewId);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(string reviewId);
        Task<bool> UpdateReviewAsync(Review review);
    }
}
