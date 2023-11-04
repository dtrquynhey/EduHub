﻿using Microsoft.EntityFrameworkCore;
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
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        public string ProfilePicture { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; } = false;

        public IEnumerable<Interaction> UserInteractions { get; set; }

        public IEnumerable<CampaignMember> Campaigns { get; set; }
    }
}
