using BookReview.Data.Entities;

namespace BookReviewSite.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<int> GenreIds { get; set; }
        public int AuthorId { get; set; }
    }
}
