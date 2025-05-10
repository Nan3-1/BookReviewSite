// UserBook.cs
using BookReview.Data.Entities;
using BookReviewSite.Data.Entities;

public class UserBook
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int BookId { get; set; }
    public BookStatusType Status { get; set; } // Favorite, CurrentlyReading, WantToRead

    // Navigation properties
    public Book Book { get; set; }
    public ApplicationUser User { get; set; }
}