using Dtos;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Models
{
    public class Profile
    {
        public string ProfileId { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public List<MovieList> MovieLists { get; set; }
        public List<Review> Reviews { get; set; }

        public Profile(string profileId,string userId, string picture, 
            List<MovieList> movieLists, List<Review> reviews)
        {
            ProfileId = profileId;
            UserId = userId;
            Picture = picture;
            MovieLists = movieLists;
            Reviews = reviews;
        }

        public Profile()
        {
        }

        public ProfileDto ToProfileDto()
        {
            return new ProfileDto
            {
                ProfileId = ProfileId,
                UserId = UserId,
                Picture = Picture,
                MovieLists = MovieLists,
                Reviews = Reviews.Select(x => x.ToReviewDto()).ToList()
            };
        }
    }
}