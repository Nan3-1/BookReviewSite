using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookReview.Data.Entities;

namespace BookReview.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BookReview.Data.Entities.Author> Author { get; set; } = default!;
        public DbSet<BookReview.Data.Entities.Book> Book { get; set; } = default!;
        public DbSet<BookReview.Data.Entities.Review> Review { get; set; } = default!;
        public DbSet<BookReview.Data.Entities.Genres> Genre { get; set; } = default!;
    }
}
