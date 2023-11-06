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
        [ForeignKey("TeacherId")]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        [Required]
        public CampaignType CampaignType { get; set; }

        public IEnumerable<CampaignMember> Members { get; set; }
        public IEnumerable<Interaction> Interactions { get; set; }
        public Engagement Engagement { get; set; }

    }
}
