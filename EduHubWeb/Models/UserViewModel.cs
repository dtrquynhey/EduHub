using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace EduHubWeb.Models
{
    public class UserViewModel : IdentityUser
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter your Username.")]
        public string Username { get; set; }

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
