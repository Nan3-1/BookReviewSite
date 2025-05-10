using System.ComponentModel;
using BookReviewSite.Data.Entities;

﻿namespace BookReviewSite.Data.Entities;
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public string ReviewerName { get; set; }
        public string ReviewContent { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public string ReviewContent { get; set; }
        public int ReviewRating { get; set; }
        public int BookId { get; set; } 

        public virtual Book? Book { get; set; }
        public object DatePosted { get; internal set; }

        public Book Book { get; set; }

    }
}
