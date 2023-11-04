using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.DataModels
{
    public class Engagement
    {
        [Key]
        public int EngagementId { get; set; }
        public int CampaignId { get; set; }

        [Required]
        public int ViewsCount { get; set; }

        [Required]
        public int LikesCount { get; set; }

        [Required]
        public int CommentsCount { get; set; }


        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }
    }
}
