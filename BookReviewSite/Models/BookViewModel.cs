using BookReviewSite.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookReviewSite.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<int> GenreIds { get; set; }
        public int AuthorId { get; set; }

        public List<Book> AllBooks { get; set; } = new List<Book>();
        public HashSet<int> FavoriteBookIds { get; set; } = new HashSet<int>();
        public HashSet<int> CurrentlyReadingIds { get; set; } = new HashSet<int>();
        public HashSet<int> WantToReadIds { get; set; } = new HashSet<int>();

        [NotMapped] // This ensures EF won't try to map it to the database
        public bool IsFavorite { get; set; }
    }
}

