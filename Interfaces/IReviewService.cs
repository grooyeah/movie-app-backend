using Dtos;
using Models;

namespace Interfaces
{
    public interface IReviewService
    {
        Task<bool> CreateReviewAsync(ReviewDto review);
        Task<bool> DeleteReviewAsync(string reviewId);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(string reviewId);
        Task<bool> UpdateReviewAsync(ReviewDto review);
    }
}
