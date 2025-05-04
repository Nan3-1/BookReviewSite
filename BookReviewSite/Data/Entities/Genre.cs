namespace BookReview.Data.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book>? Books { get; set; } = new List<Book>();    
    }

}

 