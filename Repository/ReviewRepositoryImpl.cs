using Database;
using Dtos;
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

        public async Task<bool> CreateReviewAsync(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();

            var existingReview = await GetReviewByIdAsync(review.ReviewId);
            
            if(existingReview == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteReviewAsync(string reviewId)
        {
            var reviewToRemove = await GetReviewByIdAsync(reviewId);
         
            if (reviewToRemove == null)
            {
                return false;
            }

            _dbContext.Reviews.Remove(reviewToRemove);
            await _dbContext.SaveChangesAsync();

            var existingUser = await GetReviewByIdAsync(reviewId);
            
            if(existingUser == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            var allExistingReviews = await _dbContext.Reviews.ToListAsync();

            if(allExistingReviews == null)
            {
                return null;
            }

            return allExistingReviews;
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            var existingReview = await _dbContext.Reviews.SingleOrDefaultAsync(x => x.ReviewId == reviewId);
            
            if(existingReview == null)
            {
                return null;
            }

            return existingReview;
        }

        public async Task<bool> UpdateReviewAsync(Review review)
        {
            var reviewToUpdate = review;

            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();

            var updatedReview = await GetReviewByIdAsync(review.ReviewId);

            if(updatedReview == null)
            {
                return false;
            }

            if(updatedReview != reviewToUpdate)
            {
                return false;
            }

            return true;
        }
    }
}
