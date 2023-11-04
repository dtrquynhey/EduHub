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

        [StringLength(1000, ErrorMessage = "Description must be at most 10 characters.")]
        public string Description { get; set; }

        [StringLength(4000, ErrorMessage = "Content must be at most 10 characters.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Last Modified Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please select a campaign type.")]
        public CampaignType CampaignType { get; set; }

        public IEnumerable<CampaignMember> Members { get; set; }
        public IEnumerable<Interaction> Interactions { get; set; }
        public Engagement Engagement { get; set; }

        public User User { get; set; }
    }
}
