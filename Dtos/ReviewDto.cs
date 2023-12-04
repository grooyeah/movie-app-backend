using Models;

namespace Dtos
{
    public class ReviewDto
{
        public string ImdbID { get; set; }
        public string Author { get; set; }
        public string MovieTitle { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime PublishedOn { get; set; }
        public string UserId { get; set; }

        public ReviewDto()
        {
        }

        public Review ToReview()
        {
            return new Review
            {
                ImdbID = ImdbID,
                Author = Author,
                MovieTitle = MovieTitle,
                ReviewTitle = ReviewTitle,
                ReviewText = ReviewText,
                Rating = Rating,
                PublishedOn = PublishedOn,
                UserId = UserId
            };
        }
    }
}
