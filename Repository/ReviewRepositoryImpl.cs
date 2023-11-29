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

        public  void CreateReview(Review review)
        {
            _dbContext.Reviews.Add(review);
        }

        public  void DeleteReview(string reviewId)
        {
            var reviewDb =  GetReviewById(reviewId);
            _dbContext.Reviews.Remove(reviewDb);
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _dbContext.Reviews.AsEnumerable();
        }

        public Review GetReviewById(string reviewId)
        {
            return _dbContext.Reviews.SingleOrDefault(x => x.ReviewId == reviewId);
        }

        public  void UpdateReview(Review review)
        {
            _dbContext.Reviews.Update(review);
        }
    }
}
