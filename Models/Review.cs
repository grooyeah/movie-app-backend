namespace Models
{
    public class Review
    {
        public string ReviewId { get; set; }
        public string ReviewTitle { get; set; }
        public string RProfileId { get; set; }
        public string ImdbID { get; set; }
        public string Author { get; set; }
        public string MovieTitle { get; set; }
        public string ReviewText { get; set; }
        public double Rating { get; set; }
        public DateTime PublishedOn { get; set; }
    }

}
