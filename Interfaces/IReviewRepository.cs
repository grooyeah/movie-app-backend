using Models;

namespace Interfaces
{
    public interface IReviewRepository
    {
        Task CreateReviewAsync(Review review);
        Task DeleteReviewAsync(string reviewId);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(string reviewId);
        Task UpdateReviewAsync(Review review);
    }
}
