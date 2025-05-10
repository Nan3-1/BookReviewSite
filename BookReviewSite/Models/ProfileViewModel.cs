// ViewModels/ProfileViewModel.cs
using BookReview.Data.Entities;
using BookReviewSite.Models;
using System.Collections.Generic;

namespace BookReviewSite.ViewModels
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public List<UserBookViewModels> FavoriteBooks { get; set; }
        public List<UserBookViewModels> WantToRead { get; set; }
        public List<UserBookViewModels> CurrentlyReading { get; set; }
        public List<Review> Reviews { get; set; }
    }
}