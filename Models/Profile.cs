using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Models
{
    public class Profile
    {
        public string ProfileId { get; set; }
        public User User { get; set; }
        public string Picture { get; set; }
        public List<string> FavoriteMovies { get; set; }
        public List<Review> Reviews { get; set; }

        public Profile(User user, string picture, List<string> favoriteMovies, List<Review> reviews)
        {
            User = user;
            Picture = picture;
            FavoriteMovies = favoriteMovies;
            Reviews = reviews;
        }

        public Profile()
        {
        }

    }
}