using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.DataModels
{
    public class CampaignMember
    {
        [Key]
        public int CampaignMemberId { get; set; }

        [ForeignKey("Campaign")]
        public int CampaignId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        [Required]
        public string Status { get; set; }

        public Campaign Campaign { get; set; }
        public User User { get; set; }
    }
}
