using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHubLibrary.Services
{
    public class EngagementServices : IEngagementServices
    {
        private readonly EduHubDbContext _dbContext;

        public EngagementServices(EduHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Engagement> GetEngagementByCampaignIdAsync(int campaignId)
        {
            Engagement? engagement = await _dbContext.Engagements
                .FirstOrDefaultAsync(e => e.CampaignId == campaignId);
            if (engagement != null)
            {
                return engagement;
            }
            throw new Exception();
        }

        public async Task CreateEngagementAsync(Engagement engagement)
        {
            _dbContext.Engagements.Add(engagement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEngagementAsync(Engagement engagement)
        {
            _dbContext.Entry(engagement).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
