using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduHubWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly EduHubDbContext _dbContext;
        protected readonly UserManager<IdentityUser> _userManager;

        public BaseController(EduHubDbContext dbContext,
            UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        protected async Task UpdateViewDataUsers()
        {
            var campaignsDb = await _dbContext.Campaigns
                                .Include(c => c.User)

                                .Include(c => c.Interactions)
                                .ToListAsync();
            var userIds = campaignsDb.SelectMany(c => c.Interactions.Select(i => i.UserId)).Distinct();
            var userDictionary = await _dbContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u.FirstName + u.LastName);

            ViewData["Users"] = userDictionary;
        }

        protected async Task UpdateViewDataCommentCounts()
        {
            // Retrieve all engagements
            var engagements = await _dbContext.Engagements.ToListAsync();

            // Create a dictionary with CampaignId as key and sum of CommentsCount as value
            var commentCounts = engagements
                .GroupBy(e => e.CampaignId)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(e => e.CommentsCount)
                );
            // Assign the dictionary to ViewData
            ViewData["CommentCounts"] = commentCounts;
        }

        protected async Task SetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // This will get the current user's ID
            var user = await _userManager.FindByIdAsync(userId);
            User userDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            ViewData["UserId"] = userDb.UserId.ToString();
        }

    }
}
