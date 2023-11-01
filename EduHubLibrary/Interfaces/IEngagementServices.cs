using EduHubLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Interfaces
{
    public interface IEngagementServices
    {
        Task<Engagement> GetEngagementByCampaignIdAsync(int campaignId);
        Task CreateEngagementAsync(Engagement engagement);
        Task UpdateEngagementAsync(Engagement engagement);
    }
}
