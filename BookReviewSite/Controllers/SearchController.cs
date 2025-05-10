using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookReviewSite.Models;
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
            .Where(b => b.Title.Contains(q, StringComparison.CurrentCultureIgnoreCase) ||
                        (b.Author?.FullName?.ToLower().Contains(q, StringComparison.CurrentCultureIgnoreCase) ?? false))
            .ToList();

        var filteredAuthors = _context.Author.Where(b => b.Name.Contains(q, StringComparison.CurrentCultureIgnoreCase) || b.LastName.Contains(q, StringComparison.CurrentCultureIgnoreCase))

            .ToList();

        var viewModel = new SearchResultsViewModel()
        {
            Query = q,
            Books = filteredBooks,
            Authors = filteredAuthors
        };

        return View(viewModel); 
    }
}
