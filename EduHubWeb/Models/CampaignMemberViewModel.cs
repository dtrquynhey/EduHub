using System;
using System.ComponentModel.DataAnnotations;
using EduHubLibrary.DataModels.Enums;

namespace EduHubWeb.Models
{
    public class CampaignMemberViewModel
    {
        public int CampaignMemberId { get; set; }
        public int CampaignId { get; set; }
        public int MemberId { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public Status Status { get; set; }
    }
}
