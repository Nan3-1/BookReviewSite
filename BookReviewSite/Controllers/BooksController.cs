using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookReviewSite.Data;
using BookReviewSite.Models;

using BookReviewSite.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookReviewSite.Data.Entities;
using BookReviewSite.ViewModels;

namespace BookReviewSite.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public virtual ICollection<Author>? Authors { get; set; }

        // GET: Books
        public async Task<IActionResult> Index(string searchString, string genreFilter, string authorFilter)
        {
            var booksQuery = _context.Books
                .Include(b => b.Genres)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchString))
            {

                // Simple case-insensitive search on Title and Author
                books = books.Where(b =>
                    b.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString));
            }


            if (!string.IsNullOrEmpty(genreFilter))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Genres.Any(g => g.Name == genreFilter));
            }

            if (!string.IsNullOrEmpty(authorFilter))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Author.Name.Contains(authorFilter));
            }

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
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
                 var reviews = await _context.Review
                .Where(r => r.BookId == book.BookId)
                .OrderByDescending(r => r.DatePosted)
                .ToListAsync();

            ViewBag.Reviews = reviews;
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

        private async Task<List<Book>> GetUserBooksByStatus(string userId, BookStatusType status)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .Where(b => b.BookId == id.Value)
                .FirstOrDefault();
           
            ViewData["Genres"] = new MultiSelectList(_context.Genre, "GenreId", "Name", model.GenreIds);
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "FullName", book.AuthorId);
            return View(model);
            
            var query = _context.UserBooks
                .Where(ub => ub.UserId == userId);

            query = query.Where(ub => ub.Status == status);

            return await query
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

        //// ... (Keep your existing Create, Edit, Delete actions)
    }
}