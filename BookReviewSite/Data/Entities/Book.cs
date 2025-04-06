namespace BookReview.Data.Entities
{
    public class Book
    {
        public int BookId { get; set; } 

        public string Title { get; set; }   

        public string Genre { get; set; }

        public int AuthorId { get; set; }   

        public virtual Author? Author { get; set; } 
    }
}
