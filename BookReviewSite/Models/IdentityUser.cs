using Microsoft.AspNetCore.Identity;

namespace BookReviewSite.Models
{
    // Renamed the class to avoid conflict with the existing 'ApplicationUser' definition
    public class CustomApplicationUser : IdentityUser
    {
        // Add custom properties here
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
