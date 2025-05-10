using BookReview.Data.Entities;

namespace BookReviewSite.Models
{
    public enum BookStatus
    {
        Favorite,
        WantToRead,
        CurrentlyReading
    }

    public class UserBookViewModels
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public BookStatus Status { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;

        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
    }
}
