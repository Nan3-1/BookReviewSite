using BookReview.Data.Entities;

namespace BookReviewSite.Data.Seeders
{
    public static class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            await SeedAuthors(context);
            await SeedGenres(context);
            await SeedBooks(context);

        }

        private static async Task SeedBooks(ApplicationDbContext context)
        {
            var books = new List<Book>()
            {
                new Book()
                {
                    Title = "Harry Potter and the Philosopher's Stone",
                    GenreId = 1,
                    AuthorId = 1
                }
            };
            if (context.Books.Count() == 0)
            {
                context.Books.AddRange(books);

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedGenres(ApplicationDbContext context)
        {
            var genres = new List<Genre>()
            {
                new Genre() { Name = "Fantasy"},
                new Genre() { Name = "Science Fiction" },
                new Genre() { Name = "Mystery"},
                new Genre() { Name = "Romance"},
                new Genre() { Name = "Horror" },
            };
            if (context.Genres.Count() == 0)
            {
                context.Genres.AddRange(genres);

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedAuthors(ApplicationDbContext context)
        {

            var authors = new List<Author>()
            {
                new Author()
                {
                    Name = "J.K.",
                    LastName = "Rowling",
                    Age = 50,
                    Autobiography = "J.K. Rowling is a British author, best known for writing the Harry Potter series."
                },
                new Author()
                {
                    Name = "George",
                    LastName = "Orwell",
                    Age = 46,
                    Autobiography = "George Orwell was an English novelist and essayist, known for his works on social injustice.",
                },
                new Author()
                {
                    Name = "J.R.R.",
                    LastName = "Tolkien",
                    Age = 81,
                    Autobiography = "J.R.R. Tolkien was an English writer and professor, best known for The Hobbit and The Lord of the Rings."
                },
                new Author()
                {
                    Name = "Agatha",
                    LastName = "Christie",
                    Age = 85,
                    Autobiography = "Agatha Christie was an English writer known for her detective novels and short stories."
                },
                new Author()
                {
                    Name = "Mark",
                    LastName = "Twain",
                    Age = 74,
                    Autobiography = "Mark Twain was an American writer, humorist, and lecturer, known for his novels The Adventures of Tom Sawyer and Adventures of Huckleberry Finn."
                },
            };

            if (context.Authors.Count() == 0)
            {
                context.Authors.AddRange(authors);

                await context.SaveChangesAsync();
            }
        }
    }
}
