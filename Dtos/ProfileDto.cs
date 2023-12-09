using Models;

namespace Dtos
{
    public class ProfileDto
{
        public string ProfileId { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public List<MovieList> MovieLists { get; set; }
        public List<ReviewDto> Reviews { get; set; }

        public ProfileDto(string profileId,string userId, string picture, List<MovieList> movieLists, List<ReviewDto> reviews)
        {
            UserId = userId;
            Picture = picture;
            MovieLists = movieLists;
            Reviews = reviews;
        }

        public ProfileDto()
        {
        }

        public Profile ToProfile()
        {
            return new Profile
            {
                ProfileId = ProfileId,
                UserId = UserId,
                Picture = Picture,
                MovieLists = MovieLists,
                Reviews = Reviews.Select(x => x.ToReview()).ToList()
            };
        }

    }
}
