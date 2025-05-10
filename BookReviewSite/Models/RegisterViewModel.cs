namespace BookReviewSite.Models
{
    // Removed duplicate properties and ensured only one definition of RegisterViewModel exists.  
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
