namespace BookReview.Data.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string ReviewContent { get; set; }

        public int ReviewRating { get; set; }

        public int BookId { get; set; } 

        public virtual Book? Book { get; set; } 
    }
}
