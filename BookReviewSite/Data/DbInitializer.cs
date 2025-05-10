using BookReview.Data;
using BookReviewSite.Data.Entities;

namespace BookReviewSite.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Създай базата, ако я няма
            context.Database.EnsureCreated();

            // Ако вече има книги — не правим нищо
            if (context.Book.Any())
                return;

            // Примерен автор
            var author = new Author { FullName = "George Orwell" };
            context.Author.Add(author);

            // Примерни жанрове
            var genre1 = new Genre { Name = "Dystopian" };
            var genre2 = new Genre { Name = "Science Fiction" };
            context.Genre.AddRange(genre1, genre2);

            context.SaveChanges();

            // Добави книга
            var book = new Book
            {
                Title = "1984",
                Author = author,
                Genres = new List<Genre> { genre1, genre2 }
            };

            context.Book.Add(book);
            context.SaveChanges();
        }
    }
}
