using BookReviewSite.Data.Entities;

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
                },
                new Book()
                {
                    Title = "1984",
                    GenreId = 10,
                    AuthorId = 2
                },
                new Book()
                {
                    Title = "The Hobbit",
                    GenreId = 1,
                    AuthorId = 3
                },
                new Book()
                {
                    Title = "A Game of Thrones",
                    GenreId = 12,
                    AuthorId = 4
                },
                new Book()
                {
                    Title = "The Great Gatsby",
                    GenreId = 2,
                    AuthorId = 9
                },
                new Book()
                {
                    Title = "The Catcher in the Rye",
                    GenreId = 8,
                    AuthorId = 8
                },
                new Book()
                {
                    Title = "The Shining",
                    GenreId = 5,
                    AuthorId = 7
                },
                new Book()
                {
                    Title = "To Kill a Mockingbird",
                    GenreId = 8,
                    AuthorId = 10
                },
                new Book()
                {
                    Title = "Fahrenheit 451",
                    GenreId = 10,
                    AuthorId = 11
                },
                new Book()
                {
                    Title = "The Martian Chronicles",
                    GenreId = 2,
                    AuthorId = 11
                },
                 new Book()
                 {
                    Title = "A Dance with Dragons",
                    GenreId = 12,
                    AuthorId = 12
                 },
                 new Book()
                 {
                    Title = "The Fellowship of the Ring",
                    GenreId = 12,
                    AuthorId = 3
                 },
                 new Book()
                 {
                    Title = "The Two Towers",
                    GenreId = 12,
                    AuthorId = 3
                 },
                    new Book()
                    {
                        Title = "The Return of the King",
                        GenreId = 12,
                        AuthorId = 3
                    },
                    new Book()
                    {
                          Title = "Murder on the Orient Express",
                         GenreId = 3,
                         AuthorId = 4
                    },
                    new Book()
                    {
                      Title = "IT",
                      GenreId = 5,
                      AuthorId = 7
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
                new Genre() { Name = "Science Fiction"},
                new Genre() { Name = "Mystery"},
                new Genre() { Name = "Romance"},
                new Genre() { Name = "Horror" },
                new Genre() { Name = "Thriller"},
                new Genre() { Name = "Non-Fiction"},
                new Genre() { Name = "Historical Fiction"},
                new Genre() { Name = "Biography"},
                new Genre() { Name = "Dystopian"},
                new Genre() { Name = "Adventure"},
                new Genre() { Name = "Epic-Fantasy"}
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
                    Autobiography = "George Orwell was an English novelist and essayist, known for his works on social injustice."
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
                new Author()
                {
                    Name = "Isaac",
                    LastName = "Asimov",
                    Age = 72,
                    Autobiography = "Isaac Asimov was an American author and professor of biochemistry, known for his works in science fiction and popular science."
                },
                new Author()
                {
                    Name = "Stephen",
                    LastName = "King",
                    Age = 73,
                    Autobiography = "Stephen King is an American author of horror, supernatural fiction, suspense, and fantasy novels."
                },
                new Author()
                {
                    Name = "J.D.",
                    LastName = "Salinger",
                    Age = 91,
                    Autobiography = "J.D. Salinger was an American writer known for his novel The Catcher in the Rye."
                },
                new Author()
                {
                    Name = "F. Scott",
                    LastName = "Fitzgerald",
                    Age = 44,
                    Autobiography = "F. Scott Fitzgerald was an American novelist and short story writer, known for his novel The Great Gatsby."
                },
                new Author()
                {
                    Name = "Harper",
                    LastName = "Lee",
                    Age = 89,
                    Autobiography = "Harper Lee was an American novelist, best known for her novel To Kill a Mockingbird."
                },
                new Author()
                {
                    Name = "Ray",
                    LastName = "Bradbury",
                    Age = 91,
                    Autobiography = "Ray Bradbury was an American author and screenwriter, known for his works in science fiction and fantasy."
                },
                new Author()
                {
                     Name = "George R. R.",
                     LastName = "Martin",
                     Age = 75,
                     Autobiography = "George R. R. Martin is an American novelist and short story writer, best known for his epic fantasy series A Song of Ice and Fire, which inspired the TV series Game of Thrones."
                }

            };

            if (context.Authors.Count() == 0)
            {
                context.Authors.AddRange(authors);

                await context.SaveChangesAsync();
            }
        }

    }
}