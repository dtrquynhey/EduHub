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
    public class InteractionServices : IInteractionServices
    {
        private readonly EduHubDbContext _dbContext;

        public InteractionServices(EduHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Interaction>> GetInteractionsForCampaignAsync(int campaignId)
        {
            return await _dbContext.Interactions
                .Where(i => i.CampaignId == campaignId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Interaction>> GetInteractionsByUserAsync(int userId)
        {
            return await _dbContext.Interactions
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<Dictionary<int, bool>> GetUserLikeStatus(int userId)
        {
            var likedCampaigns = await _dbContext.Interactions
                .Where(i => i.UserId == userId && i.InteractionType == InteractionType.Like)
                .ToDictionaryAsync(i => i.CampaignId, i => true);

            return likedCampaigns;
        }

        public async Task CreateInteractionAsync(Interaction interaction)
        {
            _dbContext.Interactions.Add(interaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateInteractionAsync(Interaction interaction)
        {
            _dbContext.Entry(interaction).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteInteractionAsync(int interactionId)
        {
            var interaction = await _dbContext.Interactions.FindAsync(interactionId);

            if (interaction != null)
            {
                _dbContext.Interactions.Remove(interaction);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
