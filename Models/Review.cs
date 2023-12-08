using Dtos;

namespace Models
{
    public class Review
    {
        public string ReviewId { get; set; }
        public string ProfileId { get; set; }
        public string ImdbID { get; set; }
        public string Author { get; set; }
        public string MovieTitle { get; set; }
        public string ReviewText { get; set; }
        public double Rating { get; set; }
        public DateTime PublishedOn { get; set; }
        public string ProfileId { get; set; }

        public Review()
        {
        }

        public Review(string reviewId, string imdbId,
            string authorName, string movieTitle, string reviewTitle,
            string reviewText, int rating, DateTime publishedOn, string profileId)
        {
            ReviewId = reviewId;
            ImdbId = imdbId;
            AuthorName = authorName;
            MovieTitle = movieTitle;
            ReviewTitle = reviewTitle;
            ReviewText = reviewText;
            Rating = rating;
            PublishedOn = publishedOn;
            ProfileId = profileId;
        }

        public ReviewDto ToReviewDto()
        {
            return new ReviewDto
            {
                ReviewId = ReviewId,
                ImdbId = ImdbId,
                AuthorName = AuthorName,
                MovieTitle = MovieTitle,
                ReviewTitle = ReviewTitle,
                Review = ReviewText,
                Rating = Rating,
                PublishedOn = PublishedOn,
                ProfileId = ProfileId
            };
        }
    }

}
