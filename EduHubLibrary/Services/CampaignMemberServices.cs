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
    public class CampaignMemberServices : ICampaignMemberServices
    {
        private readonly EduHubDbContext _dbContext;

        public CampaignMemberServices(EduHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CampaignMember> GetCampaignMemberByIdAsync(int campaignMemberId)
        {
            CampaignMember? cm = await _dbContext.CampaignMembers.FindAsync(campaignMemberId);
            if (cm != null)
            {
                return cm;
            }
            throw new Exception();
        }

        public async Task<IEnumerable<CampaignMember>> GetMembersOfCampaignAsync(int campaignId)
        {
            return await _dbContext.CampaignMembers
                .Where(cm => cm.CampaignId == campaignId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignMember>> GetCampaignsForUserAsync(int userId)
        {
            return await _dbContext.CampaignMembers
                .Where(cm => cm.MemberId == userId)
                .ToListAsync();
        }

        public async Task AddUserToCampaignAsync(int userId, int campaignId)
        {
            var newCampaignMember = new CampaignMember
            {
                CampaignId = campaignId,
                MemberId = userId,
                JoinDate = DateTime.Now,
                Status = Status.Pending // You can set the initial status as needed
            };

            _dbContext.CampaignMembers.Add(newCampaignMember);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveUserFromCampaignAsync(int userId, int campaignId)
        {
            var campaignMember = await _dbContext.CampaignMembers
                .FirstOrDefaultAsync(cm => cm.CampaignId == campaignId && cm.MemberId == userId);

            if (campaignMember != null)
            {
                _dbContext.CampaignMembers.Remove(campaignMember);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateMembershipStatus(int userId, Status status)
        {
            var campaignMembers = await _dbContext.CampaignMembers
                .Where(cm => cm.MemberId == userId)
                .ToListAsync();

            foreach (var campaignMember in campaignMembers)
            {
                campaignMember.Status = status;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsUserMemberOfCampaignAsync(int userId, int campaignId)
        {
            return await _dbContext.CampaignMembers
                .AnyAsync(cm => cm.CampaignId == campaignId && cm.MemberId == userId);
        }
    }
}
