using BookReview.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookReviewSite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public virtual ICollection<UserBookViewModels> UserBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}