using System.ComponentModel.DataAnnotations;

namespace BookReview.Data.Entities
{
    public class Genres
    {
        public class Genre
        {
            [Key]
            public int GenreId { get; set; }

            [Required(ErrorMessage = "Genre name is required.")]
            [StringLength(100, ErrorMessage = "Genre name cannot be longer than 100 characters.")]
            public string Name { get; set; }

            // Navigation property for related books
            public virtual ICollection<Book>? Books { get; set; }
        }
        public class Book
        {
            [Key]
            public int BookId { get; set; }

            [Required(ErrorMessage = "Book title is required.")]
            [StringLength(200)]
            public string Title { get; set; }

            // Foreign key to Genre
            public int GenreId { get; set; }

            // Navigation property to Genre
            public virtual Genre? Genre { get; set; }

            // Additional properties e.g. AuthorId and Author can be added here
        }
    }

}

