using Models;

namespace Dtos
{
    public class ReviewDto
{
        public string ReviewId { get; set; }
        public string ImdbId { get; set; }
        public string AuthorName { get; set; }
        public string MovieTitle { get; set; }
        public string ReviewTitle { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime PublishedOn { get; set; }
        public string ProfileId { get; set; }
        public ReviewDto()
        {
        }

        public ReviewDto(string reviewId, string imdbId, 
            string authorName, string movieTitle, string reviewTitle,
            string review, int rating, DateTime publishedOn, string profileId)
        {
            ReviewId = reviewId;
            ImdbId = imdbId;
            AuthorName = authorName;
            MovieTitle = movieTitle;
            ReviewTitle = reviewTitle;
            Review = review;
            Rating = rating;
            PublishedOn = publishedOn;
            ProfileId = profileId;
        }

        public Review ToReview()
        {
            return new Review
            {
                ReviewId = ReviewId,
                ImdbId = ImdbId,
                AuthorName = AuthorName,
                MovieTitle = MovieTitle,
                ReviewTitle = ReviewTitle,
                ReviewText = Review,
                Rating = Rating,
                PublishedOn = PublishedOn,
                ProfileId = ProfileId
            };
        }
    }
}
