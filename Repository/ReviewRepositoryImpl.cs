using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class ReviewRepositoryImpl : IReviewRepository
    {
        private readonly UserDbContext _dbContext;

        public ReviewRepositoryImpl(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateReviewAsync(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(string reviewId)
        {
            var reviewDb = await GetReviewByIdAsync(reviewId);
            if (reviewDb != null)
            {
                _dbContext.Reviews.Remove(reviewDb);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _dbContext.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            return await _dbContext.Reviews.SingleOrDefaultAsync(x => x.ReviewId == reviewId);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();
        }
    }
}
