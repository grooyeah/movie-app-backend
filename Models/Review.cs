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

        public Review()
        {
        }

        public Review(string reviewId, string profileId, string imdbID, string author, string movieTitle, string reviewText, double rating, DateTime publishedOn)
        {
            ReviewId = reviewId;
            ProfileId = profileId;
            ImdbID = imdbID;
            Author = author;
            MovieTitle = movieTitle;
            ReviewText = reviewText;
            Rating = rating;
            PublishedOn = publishedOn;
        }
    }

}
