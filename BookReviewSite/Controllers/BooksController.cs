using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BookReviewSite.Data;
using BookReviewSite.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookReviewSite.ViewModels;
using System.Security.Claims;
using BookReviewSite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookReviewSite.Controllers
{
    public class BooksController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public virtual ICollection<Author>? Authors { get; set; }

        // GET: Books
        public async Task<IActionResult> Index(string searchString, string genreFilter, string authorFilter)
        {
            // Start with base query including related entities
            var booksQuery = _context.Books
                .Include(b => b.Genres)
                .Include(b => b.Author)  // Added Author inclusion
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                // Search both Title and Author's name
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString) ||
                    (b.Author != null && b.Author.Name.Contains(searchString)));
            }

            // Apply genre filter
            if (!string.IsNullOrEmpty(genreFilter))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Genres.Any(g => g.Name == genreFilter));
            }

            // Apply author filter
            if (!string.IsNullOrEmpty(authorFilter))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Author != null &&
                    b.Author.Name.Contains(authorFilter));
            }

            // Execute query
            var books = await booksQuery.ToListAsync();

            return View(books);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Genres)
                .Include(b => b.Reviews)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            // Fetch reviews for the book
            var reviews = await _context.Reviews
                .Where(r => r.BookId == book.BookId)
                .OrderByDescending(r => r.DatePosted) // Perform ordering on the queryable collection
                .ToListAsync(); // Convert to a list asynchronously

            ViewBag.Reviews = reviews; // Pass reviews to the view

            return View(book);
        }


        // GET: Books/MyBooks


        [Authorize]
        public async Task<IActionResult> MyBooks()
        {
            var userId = _userManager.GetUserId(User);

            var model = new MyBooksViewModel
            {
                FavoriteBooks = await GetUserBooksByStatus(userId, BookStatusType.Favorite),
                CurrentlyReading = await GetUserBooksByStatus(userId, status: BookStatusType.CurrentlyReading),
                WantToRead = await GetUserBooksByStatus(userId, status: BookStatusType.WantToRead)
            };

            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> FavoriteBooks()
        {
            var userId = _userManager.GetUserId(User);
            var favoriteBooks = await _context.UserBooks
                .Where(ub => ub.UserId == userId && ub.Status == BookStatusType.Favorite)
                .Select(ub => ub.Book)
                .ToListAsync();

            return View(favoriteBooks);
        }
        public async Task<IActionResult> CurrentlyReading()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentlyReading = await _context.UserBooks
                .Where(ub => ub.UserId == userId && ub.Status == BookStatusType.CurrentlyReading)
                .Select(ub => ub.Book)
                .ToListAsync();

            return View(currentlyReading);
        }
        public async Task<IActionResult> WantsToRead()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wantsToRead = await _context.UserBooks
                .Where(ub => ub.UserId == userId && ub.Status == BookStatusType.WantToRead)
                .Select(ub => ub.Book)
                .ToListAsync();

            return View(wantsToRead);
        }
        private async Task<List<Book>> GetUserBooksByStatus(string userId, BookStatusType status)
        {
            return await _context.UserBooks
                .Where(ub => ub.UserId == userId && ub.Status == status)
                .Include(ub => ub.Book)
                    .ThenInclude(b => b.Author)
                .Include(ub => ub.Book)
                    .ThenInclude(b => b.Reviews)
                .Select(ub => ub.Book)
                .ToListAsync();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateReadingStatus(int bookId, BookStatusType status, string returnUrl = null)
        {
            var userId = _userManager.GetUserId(User);
            var userBook = await _context.UserBooks
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);

            if (userBook == null)
            {
                userBook = new UserBook
                {
                    UserId = userId,
                    BookId = bookId,
                    Status = status
                };
                _context.UserBooks.Add(userBook);
            }
            else
            {
                userBook.Status = status;
            }

            await _context.SaveChangesAsync();
            return Redirect(returnUrl ?? Url.Action(nameof(Index)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromList(int bookId)
        {
            var userId = _userManager.GetUserId(User);
            var userBook = await _context.UserBooks
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);

            if (userBook != null)
            {
                _context.UserBooks.Remove(userBook);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyBooks));
        }
        public IActionResult Create()
        {
            ViewData["Genres"] = new MultiSelectList(_context.Genres, "GenreId", "Name");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genres = new List<Genre>();
                foreach (var genreId in model.GenreIds)
                {
                    var genre = await _context.Genres.FindAsync(genreId);
                    if (genre != null)
                    {
                        genres.Add(genre);
                    }
                }
                Book book = new Book()
                {
                    BookId = model.BookId,
                    Title = model.Title,
                    AuthorId = model.AuthorId,
                    Genres = genres
                };

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "FullName", model.AuthorId);
            return View(model);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .Where(b => b.BookId == id.Value)
                .FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }

            var model = new BookViewModel()
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                GenreIds = book.Genres
                .Select(g => g.GenreId).ToList()
            };
            ViewData["Genres"] = new MultiSelectList(_context.Genres, "GenreId", "Name", model.GenreIds);
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "FullName", book.AuthorId);
            return View(model);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel model)
        {
            if (id != model.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var genres = new List<Genre>();
                foreach (var genreId in model.GenreIds)
                {
                    var genre = await _context.Genres.FindAsync(genreId);
                    if (genre != null)
                    {
                        genres.Add(genre);
                    }
                }
                var bookOriginal = await _context.Books
                    .Include(b => b.Genres)
                    .FirstOrDefaultAsync(b => b.BookId == id);

                if (bookOriginal != null)
                {
                    bookOriginal.Genres.Clear();
                    bookOriginal.Title = model.Title;
                    bookOriginal.AuthorId = model.AuthorId;
                    _context.Books.Update(bookOriginal);
                    await _context.SaveChangesAsync();
                    foreach (var genre in genres)
                    {
                        bookOriginal.Genres.Add(genre);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["Genres"] = new MultiSelectList(_context.Genres, "GenreId", "Name", model.GenreIds);

            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "FullName", model.AuthorId);
            return View(model);
        }
        

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}