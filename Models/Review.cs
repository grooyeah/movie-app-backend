﻿using Dtos;

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
        public string UserId { get; set; }

        public Review()
        {
        }

        public ReviewDto ToReviewDto()
        {
            return new ReviewDto
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
