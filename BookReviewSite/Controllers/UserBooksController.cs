using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookReviewSite.Data;
using BookReviewSite.Data.Entities;
using System.Security.Claims;

namespace BookReviewSite.Controllers
{
    [Authorize]
    public class UserBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a book to a specific list
        [HttpPost]
        public async Task<IActionResult> AddToList(int bookId, BookStatusType status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
                userBook.Status = status; // Update the status if the book already exists
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("MyBooks", "Books");
        }

        // Remove a book from a specific list
        [HttpPost]
        public async Task<IActionResult> RemoveFromList(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userBook = await _context.UserBooks
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);

            if (userBook != null)
            {
                _context.UserBooks.Remove(userBook);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyBooks", "Books");
        }
    }
}

