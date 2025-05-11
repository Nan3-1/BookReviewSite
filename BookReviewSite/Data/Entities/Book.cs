using BookReviewSite.Data.Entities;
namespace BookReviewSite.Data.Entities
{ 

using System.ComponentModel.DataAnnotations.Schema;


public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public virtual ICollection<Genre>? Genres { get; set; } = new List<Genre>();
    public virtual ICollection<Review>? Reviews { get; set; }
    public double AverageRating
    {
        get
        {
            return Reviews?.Count > 0 ? Reviews.Average(r => r.ReviewRating) : 0;
        }
    }
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    public virtual Author? Author { get; set; }

}
}
    