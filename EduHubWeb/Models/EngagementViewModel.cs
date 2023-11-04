using System;
using System.ComponentModel.DataAnnotations;

namespace EduHubWeb.Models
{
    public class EngagementViewModel
    {
        public int EngagementId { get; set; }
        public int CampaignId { get; set; }

        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
    }
}
