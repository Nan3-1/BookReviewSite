// ViewModels/ProfileViewModel.cs
using BookReviewSite.Data.Entities;
using BookReviewSite.Models;
using System.Collections.Generic;

namespace BookReviewSite.ViewModels
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public List<UserBook> FavoriteBooks { get; set; }
        public List<UserBook> WantToRead { get; set; }
        public List<UserBook> CurrentlyReading { get; set; }
        public List<Review> Reviews { get; set; }
    }
}