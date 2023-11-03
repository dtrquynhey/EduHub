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
    public class Interaction
    {
        [Key]
        public int InteractionId { get; set; }
        public int CampaignId { get; set; }
        public int UserId { get; set; }

        [Required]
        public InteractionType InteractionType { get; set; }

        [Required]
        public DateTime InteractionDate { get; set; }

        public string Content { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
