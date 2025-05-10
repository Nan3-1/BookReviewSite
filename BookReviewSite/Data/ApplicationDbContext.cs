using BookReviewSite.Data.Entities;
using BookReviewSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookReviewSite.Data
{
    public class ApplicationDbContext : IdentityDbContext // Use your custom user
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<UserBook>()
                .HasKey(ub => new { ub.UserId, ub.BookId, ub.Status });
            modelBuilder.Entity<Author>(entity =>
                {
                    entity.Property(a => a.Autobiography).HasMaxLength(1000); // Adjust the size as needed
                });
            

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        // Add your other DbSets here
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}

