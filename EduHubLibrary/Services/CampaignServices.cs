using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubLibrary.DataModels.Enums;
using EduHubLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Services
{
    public class CampaignServices : Interfaces.ICampaignServices
    {
        private readonly EduHubDbContext _dbContext;

        public CampaignServices(EduHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Campaign> GetCampaignByCampaignIdAsync(int campaignId)
        {
            Campaign? campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            if (campaign != null)
            {
                return campaign;
            }
            throw new Exception();
        }

        public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
        {
            return await _dbContext.Campaigns.ToListAsync();
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsByTeacherIdAsync(int teacherId)
        {
            return await _dbContext.Campaigns
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsByTypeAsync(CampaignType campaignType)
        {
            return await _dbContext.Campaigns
                .Where(c => c.CampaignType == campaignType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
        {
            return await _dbContext.Campaigns
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task CreateCampaignAsync(Campaign campaign)
        {
            _dbContext.Campaigns.Add(campaign);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCampaignAsync(int campaignId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            if (campaign != null)
            {
                _dbContext.Campaigns.Remove(campaign);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCampaignAsync(Campaign campaign)
        {
            _dbContext.Entry(campaign).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CampaignMember>> GetCampaignMembersAsycn(int campaignId)
        {
            return await _dbContext.CampaignMembers
                .Where(cm => cm.CampaignId == campaignId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Interaction>> GetCampaignInteractionsAsycn(int campaignId)
        {
            return await _dbContext.Interactions
                .Where(i => i.CampaignId == campaignId)
                .ToListAsync();
        }

        public async Task<Engagement> GetCampaignEngagementAsycn(int campaignId)
        {
            Engagement? engagement = await _dbContext.Engagements
                .Where(e => e.CampaignId == campaignId)
                .FirstOrDefaultAsync();
            if (engagement != null)
            {
                return engagement;
            }
            throw new Exception();
        }

        public async Task<bool> IsUserMemberOfCampaignAsync(int userId, int campaignId)
        {
            return await _dbContext.CampaignMembers
                .AnyAsync(cm => cm.CampaignId == campaignId && cm.MemberId == userId);
        }

        public async Task<bool> IsUserTeacherOfCampaignAsync(int userId, int campaignId)
        {
            return await _dbContext.Campaigns
                .AnyAsync(c => c.CampaignId == campaignId && c.TeacherId == userId);
        }

        public async Task<bool> IsCampaignActiveAsync(int campaignId)
        {
            return await _dbContext.Campaigns
                .Where(c => c.CampaignId == campaignId && c.IsActive)
                .AnyAsync();
        }
    }
}
