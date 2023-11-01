using EduHubLibrary.DataModels;
using EduHubLibrary.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Interfaces
{
    public interface ICampaignServices
    {
        Task<Campaign> GetCampaignByCampaignIdAsync(int campaignId);
        Task<IEnumerable<Campaign>> GetAllCampaignsAsync();
        Task<IEnumerable<Campaign>> GetCampaignsByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Campaign>> GetCampaignsByTypeAsync(CampaignType campaignType);
        Task<IEnumerable<Campaign>> GetActiveCampaignsAsync();

        Task CreateCampaignAsync(Campaign campaign);
        Task DeleteCampaignAsync(int campaignId);
        Task UpdateCampaignAsync(Campaign campaign);

        Task<IEnumerable<CampaignMember>> GetCampaignMembersAsycn(int campaignId);
        Task<IEnumerable<Interaction>> GetCampaignInteractionsAsycn(int campaignId);
        Task<Engagement> GetCampaignEngagementAsycn(int campaignId);

        Task<bool> IsUserMemberOfCampaignAsync(int userId, int campaignId);
        Task<bool> IsUserTeacherOfCampaignAsync(int userId, int campaignId);
        Task<bool> IsCampaignActiveAsync(int campaignId);
    }
}
