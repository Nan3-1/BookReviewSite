using System.Collections.Generic;
using BookReview.Data.Entities;
using BookReviewSite.Models;

namespace BookReviewSite.ViewModels
{
    public class MyBooksViewModel
    {
        public List<Book> FavoriteBooks { get; set; }
        public List<Book> CurrentlyReading { get; set; }
        public List<Book> WantToRead { get; set; }
    }
}