using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduHubLibrary.DataModels.Enums;

namespace EduHubLibrary.DataModels
{
    public class CampaignMember
    {
        [Key]
        public int CampaignMemberId { get; set; }
        public int CampaignId { get; set; }        
        public int MemberId { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        [ForeignKey("MemberId")]
        public User user { get; set; }
    }
}
