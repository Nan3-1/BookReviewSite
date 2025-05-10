using Microsoft.AspNetCore.Mvc;
using BookReview.Data;
using Microsoft.EntityFrameworkCore;
using BookReview.Models;
using BookReview.Data.Entities;
using BookReviewSite.Data;

public class SearchController : Controller // Ensure the controller inherits from Controller
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string q)
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .ToListAsync();

        var filteredBooks = books
            .Where(b => b.Title.ToLower().Contains(q.ToLower()) ||
                        (b.Author?.FullName?.ToLower().Contains(q.ToLower()) ?? false))
            .ToList();

        var filteredAuthors = _context.Authors.Where(b => b.Name.ToLower().Contains(q.ToLower()) || b.LastName.ToLower().Contains(q.ToLower()))
            .ToList();

        var viewModel = new SearchResultsViewModel()
        {
            Query = q,
            Books = filteredBooks,
            Authors = filteredAuthors
        };

        return View(viewModel); // ✅ This matches the @model in your view
    }
}
