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
    public class InteractionsController : BaseController
    {
        private readonly DataMappingService _dataMappingService;

        public InteractionsController(EduHubDbContext dbContext,
            DataMappingService dataMappingService,
            UserManager<IdentityUser> userManager) 
            : base(dbContext, userManager)
        {
            _dataMappingService = dataMappingService;
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

                // Update Engagement for Likes
                var engagement = await _dbContext.Engagements.FirstOrDefaultAsync(e => e.CampaignId == campaignId);
                if (engagement == null)
                {
                    engagement = new Engagement { CampaignId = campaignId, LikesCount = 1, CommentsCount = 0, ViewsCount = 0 };
                    _dbContext.Engagements.Add(engagement);
                }
                else
                {
                    engagement.LikesCount++; // Increment likes count
                }
                await _dbContext.SaveChangesAsync();
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

            var engagement = await _dbContext.Engagements.FirstOrDefaultAsync(e => e.CampaignId == campaignId);
            if (engagement == null)
            {
                engagement = new Engagement { CampaignId = campaignId, LikesCount = 0, CommentsCount = 1, ViewsCount = 0 };
                _dbContext.Engagements.Add(engagement);
            }
            else
            {
                engagement.CommentsCount++; // Increment comments count
            }

            await _dbContext.SaveChangesAsync();

            await UpdateViewDataUsers();
            await UpdateViewDataCommentCounts();

            //var comments = await _dbContext.Interactions
            //                            .Where(i => i.CampaignId == campaignId
            //                            && i.InteractionType == InteractionType.Comment)
            //                            .ToListAsync();
            return PartialView("~/Views/Newsfeed/_CommentPartial.cshtml", interactionDb);
        }


        [HttpPost]
        public async Task<JsonResult> DeleteComment(int commentId)
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

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Comment not found or unauthorized." });
        }


    }
}
