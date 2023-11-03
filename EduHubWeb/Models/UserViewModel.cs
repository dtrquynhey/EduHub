using System;
using System.ComponentModel.DataAnnotations;

namespace EduHubWeb.Models
{
    public class UserViewModel
    {
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your Email Address.")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter your Username.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your Password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Password should be minimum length of 8 chars.")]
        public string PasswordHash { get; set; }

        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "Please enter your Firstname.")]
        public string FirstName { get; set; }

        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "Please enter your Lastname.")]
        public string LastName { get; set; }

        [Display(Name = "Profile Picture")]
        [DataType(DataType.ImageUrl)]
        public string ProfilePicture { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
