using System;
using System.ComponentModel.DataAnnotations;
using EduHubLibrary.DataModels.Enums;

namespace EduHubWeb.Models
{
    public class InteractionViewModel
    {
        public int InteractionId { get; set; }
        public int CampaignId { get; set; }
        public int UserId { get; set; }

        public InteractionType InteractionType { get; set; }

        [DataType(DataType.Date)]
        public DateTime InteractionDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
