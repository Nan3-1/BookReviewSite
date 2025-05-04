namespace BookReview.Data.Entities
{
    public class Book
    {
        public int BookId { get; set; } 
        public string Title { get; set; }   
        public virtual ICollection<Genre>? Genres { get; set; } = new List<Genre>();
        public int AuthorId { get; set; }   
        public virtual Author? Author { get; set; }

       
    }
}
    