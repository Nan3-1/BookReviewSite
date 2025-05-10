using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using BookReviewSite.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using BookReviewSite.Data;
using BookReviewSite.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookReviewSite.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ProfileViewModel
            {
                UserName = user.UserName,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture,
                FavoriteBooks = _context.UserBooks
                    .Where(ub => ub.UserId == user.Id && ub.Status == BookStatus.Favorite)
                    .Take(3)
                    .ToList(),
                WantToRead = _context.UserBooks
                    .Where(ub => ub.UserId == user.Id && ub.Status == BookStatus.WantToRead)
                    .Take(3)
                    .ToList(),
                CurrentlyReading = _context.UserBooks
                    .Where(ub => ub.UserId == user.Id && ub.Status == BookStatus.CurrentlyReading)
                    .Take(3)
                    .ToList(),
                Reviews = _context.Reviews
                    .Where(r => r.UserId == user.Id)
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(5)
                    .ToList()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new EditProfileViewModel
            {
                UserName = user.UserName,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                user.UserName = model.UserName;
                user.Bio = model.Bio;
                user.ProfilePicture = model.ProfilePicture;

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // In ProfileController
        [HttpPost]
        public async Task<IActionResult> AddBook(int bookId, BookStatus status)
        {
            var user = await _userManager.GetUserAsync(User);
            var userBook = new UserBookViewModels
            {
                UserId = user.Id,
                BookId = bookId,
                Status = status
            };

            _context.UserBooks.Add(userBook);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBook(int bookId, BookStatus status)
        {
            var user = await _userManager.GetUserAsync(User);
            var userBook = await _context.UserBooks
                .FirstOrDefaultAsync(ub => ub.UserId == user.Id &&
                                          ub.BookId == bookId &&
                                          ub.Status == status);

            if (userBook != null)
            {
                _context.UserBooks.Remove(userBook);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}