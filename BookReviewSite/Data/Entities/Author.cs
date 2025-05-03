namespace BookReview.Data.Entities
{
    public class Author
    {
       public int AuthorId  { get; set; }
       public string Name { get; set; }
       public string LastName { get; set; }
       public string FullName => $"{Name} {LastName}";
        public int Age { get; set; }
        public virtual ICollection<Book>? Books { get; set; }   
    }
}
