using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace EduHubWeb.Models
{
    public class UserViewModel : IdentityUser
    {
        
        public string Email {  get; set; }

        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "The FirstName field is required.")]
        public string FirstName { get; set; }


        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "The LastName field is required.")]
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }
    }

}
