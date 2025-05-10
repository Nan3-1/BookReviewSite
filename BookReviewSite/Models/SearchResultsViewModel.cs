using BookReviewSite.Data.Entities;

namespace BookReview.Models
{

    public class SearchResultsViewModel
    {
        public string Query { get; set; }
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
    }
}