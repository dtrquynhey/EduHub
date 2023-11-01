using EduHubLibrary.DataModels;
using EduHubLibrary.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Interfaces
{
    public interface ICampaignMemberServices
    {
        Task<CampaignMember> GetCampaignMemberByIdAsync(int campaignMemberId);
        Task<IEnumerable<CampaignMember>> GetMembersOfCampaignAsync(int campaignId);
        Task<IEnumerable<CampaignMember>> GetCampaignsForUserAsync(int userId);
        Task AddUserToCampaignAsync(int userId, int campaignId); 
        Task RemoveUserFromCampaignAsync(int userId, int campaignId);
        Task UpdateMembershipStatus(int userId, Status status);
        Task<bool> IsUserMemberOfCampaignAsync(int userId, int campaignId);
    }
}
