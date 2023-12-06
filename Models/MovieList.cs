namespace Models
{
    public class MovieList
    {
        public string MovieListId { get; set; }
        public string ProfileId { get; set; }
        // other properties

        public Profile Profile { get; set; }
    }
}
