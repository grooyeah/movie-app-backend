using Models;

namespace Dtos
{
    public class ProfileDto
{
        public UserDto User { get; set; }
        public string Picture { get; set; }
        public List<string> FavoriteMovies { get; set; }
        public List<ReviewDto> Reviews { get; set; }

        public ProfileDto(UserDto user, string picture, List<string> favoriteMovies, List<ReviewDto> reviews)
        {
            User = user;
            Picture = picture;
            FavoriteMovies = favoriteMovies;
            Reviews = reviews;
        }

        public ProfileDto()
        {
        }

        public Profile ToProfile()
        {
            return new Profile
            {
                User = User.ToUser(),
                Picture = Picture,
                FavoriteMovies = FavoriteMovies,
                Reviews = Reviews.Select(reviewDto => reviewDto.ToReview()).ToList()
            };
        }

    }
}
