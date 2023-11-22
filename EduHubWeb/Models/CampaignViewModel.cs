using System;
using System.Collections.Generic;
using EduHubLibrary.DataModels.Enums;
using System.ComponentModel.DataAnnotations;
using EduHubLibrary.DataModels;

namespace EduHubWeb.Models
{
    public class CampaignViewModel
    {
        public int CampaignId { get; set; }

        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 255 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description must be at most 10 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Content must be at most 10 characters.")]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Please select a campaign type.")]
        public CampaignType CampaignType { get; set; }

        public IEnumerable<CampaignMember> Members { get; set; }
        public IEnumerable<Interaction> Interactions { get; set; }
        public IEnumerable<Interaction> Comments { get; set; }
        public Engagement Engagement { get; set; }

        public User User { get; set; }
    }
}
