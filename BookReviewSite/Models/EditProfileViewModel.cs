using System.ComponentModel.DataAnnotations;

namespace BookReviewSite.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Bio")]
        [StringLength(500)]
        public string Bio { get; set; }

        [Display(Name = "Profile Picture URL")]
        [Url]
        public string ProfilePicture { get; set; }
    }
}