using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels.Enums;
using EduHubLibrary.DataModels;
using EduHubLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduHubWeb.Models;
using System.Security.Claims;
using NuGet.DependencyResolver;

namespace EduHubWeb.Controllers
{
    //[Route("[controller]")]
    public class InteractionsController : Controller
    {
        private readonly ILogger<InteractionsController> _logger;
        private readonly EduHubDbContext _dbContext;
        private readonly DataMappingService _dataMappingService;
        private readonly InteractionServices _interactionServices;
        private readonly UserManager<IdentityUser> _userManager;

        public InteractionsController(EduHubDbContext dbContext,
            DataMappingService dataMappingService,
            UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _dataMappingService = dataMappingService;
            _userManager = userManager;
        }

        [HttpPost("Interactions/Like")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(int campaignId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            User userDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (user == null)
            {
                return NotFound();
            }

            var existingInteraction = await _dbContext.Interactions.FirstOrDefaultAsync(
                 i => i.CampaignId == campaignId && i.UserId == userDb.UserId && i.InteractionType == InteractionType.Like);

            if (existingInteraction != null)
            {
                // User has already liked this campaign, so remove the like
                _dbContext.Interactions.Remove(existingInteraction);
            }
            else
            {
                var interactionViewModel = new InteractionViewModel
                {
                    CampaignId = campaignId,
                    UserId = userDb.UserId,
                    InteractionType = InteractionType.Like,
                    InteractionDate = DateTime.UtcNow,
                    Content = ""
                };

                var interactionDb = _dataMappingService.MapInteractionViewModelToInteraction(interactionViewModel);

                _dbContext.Interactions.Add(interactionDb);
            }
            await _dbContext.SaveChangesAsync();

            // Redirect back to the newsfeed or to a confirmation page
            return RedirectToAction("Index", "Newsfeed", new { id = campaignId, likeSuccess = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(int campaignId, string content)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            User userDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (userDb == null)
            {
                return NotFound();
            }

            var interactionViewModel = new InteractionViewModel
            {
                CampaignId = campaignId,
                UserId = userDb.UserId,
                InteractionType = InteractionType.Comment,
                InteractionDate = DateTime.UtcNow,
                Content = content
            };

            var interactionDb = _dataMappingService.MapInteractionViewModelToInteraction(interactionViewModel);

            _dbContext.Interactions.Add(interactionDb);
            await _dbContext.SaveChangesAsync();

            // Redirect back to the campaign detail or to a confirmation page
            return RedirectToAction("Index", "Newsfeed", new { id = campaignId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            User userDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            var comment = await _dbContext.Interactions.FindAsync(commentId);

            if (comment != null && comment.UserId == userDb.UserId)
            {
                _dbContext.Interactions.Remove(comment);
                await _dbContext.SaveChangesAsync();

                // Update the engagement count after deleting the comment
                var engagement = await _dbContext.Engagements.FirstOrDefaultAsync(e => e.CampaignId == comment.CampaignId);
                if (engagement != null)
                {
                    engagement.CommentsCount--; // Decrement the count
                    await _dbContext.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Newsfeed");
            }

            return NotFound();
        }

    }
}
