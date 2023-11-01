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

        [ForeignKey("Campaign")]
        public int CampaignId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public InteractionType InteractionType { get; set; }

        [Required]
        public DateTime InteractionDate { get; set; }

        public string Content { get; set; }

        public Campaign Campaign { get; set; }

        public User User { get; set; }
    }

}
