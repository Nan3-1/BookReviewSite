using BookReviewSite.Data.Entities;
using BookReviewSite.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookReviewSite.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public virtual ICollection<UserBookViewModels> UserBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}