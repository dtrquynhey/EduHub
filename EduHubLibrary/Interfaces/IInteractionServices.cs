using EduHubLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Interfaces
{
    public interface IInteractionServices
    {
        Task<IEnumerable<Interaction>> GetInteractionsForCampaignAsync(int campaignId);
        Task<IEnumerable<Interaction>> GetInteractionsByUserAsync(int userId);
        Task CreateInteractionAsync(Interaction interaction);
        Task UpdateInteractionAsync(Interaction interaction);
        Task DeleteInteractionAsync(int interactionId);
    }
}
