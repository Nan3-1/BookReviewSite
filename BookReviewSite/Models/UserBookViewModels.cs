using BookReviewSite.Data.Entities;

namespace BookReviewSite.Models
{
    public class UserBookViewModels
    {
        internal object? IsFavorite;

        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public BookStatusType Status { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;

        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
    }
}
