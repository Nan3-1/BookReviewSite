using System.ComponentModel;
using BookReview.Data.Entities;
using BookReviewSite.Models;

namespace BookReview.Data.Entities
{
    public class Review
    {
        internal readonly int ReviewId;

        public int Id { get; set; }
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
