using System.ComponentModel;
using BookReview.Data.Entities;
using BookReviewSite.Data.Entities;

namespace BookReview.Data.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationships
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string ReviewContent { get; set; }
        public int ReviewRating { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
