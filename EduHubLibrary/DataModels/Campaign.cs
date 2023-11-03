using EduHubLibrary.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.DataModels
{
    public class Campaign
    {
        [Key]
        public int CampaignId { get; set; }
        public int TeacherId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public CampaignType CampaignType { get; set; }

        public List<CampaignMember> Members { get; set; }
        public List<Interaction> Interactions { get; set; }
        public Engagement Engagement { get; set; }

        [ForeignKey("TeacherId")]
        public User User { get; set; }


    }
}
