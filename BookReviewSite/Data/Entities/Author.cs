namespace BookReview.Data.Entities
{
    public class Author
    {
       public int AuthorId  { get; set; }
       public string Name { get; set; }
       public string LastName { get; set; }
       public int Age { get; set; }
       public string Autobiography { get; set; } 
        public virtual ICollection<Book>? Books { get; set; }   
    }
}
