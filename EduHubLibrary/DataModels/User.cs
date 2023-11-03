using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.DataModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }         

        [Required]
        //[Index(IsUnique = true)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
        
        public List<Interaction> UserInteractions { get; set; }

        public List<CampaignMember> Campaigns { get; set; }
    }
}
