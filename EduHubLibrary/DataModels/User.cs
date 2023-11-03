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
        [Column(Order = 0)]
        public int UserId { get; set; }

        [Required]
        [Column(Order = 1)]
        public string Email { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
        
        public IEnumerable<Interaction> UserInteractions { get; set; }

        public IEnumerable<CampaignMember> Campaigns { get; set; }
    }
}
