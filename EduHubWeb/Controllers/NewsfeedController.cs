using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubLibrary.DataModels.Enums;
using EduHubLibrary.Services;
using EduHubWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduHubWeb.Controllers
{
    public class NewsfeedController : BaseController
    {
        private readonly DataMappingService _dataMappingService;
        private readonly InteractionServices _interactionServices;


        public NewsfeedController(EduHubDbContext dbContext,
            DataMappingService dataMappingService,
            InteractionServices interactionServices,
            UserManager<IdentityUser> userManager) : base(dbContext, userManager)
        {
            _dataMappingService = dataMappingService;
            _interactionServices = interactionServices;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            User userDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            var userLikeStatus = await _interactionServices.GetUserLikeStatus(userDb.UserId);
            ViewBag.UserLikeStatus = userLikeStatus;


            List<CampaignViewModel> campaignsList = new List<CampaignViewModel>();
            var campaignsDb = await _dbContext.Campaigns
                                .Include(c => c.User)

                                .Include(c => c.Interactions)
                                .ToListAsync();

            foreach (var campaign in campaignsDb)
            {
                var campaignViewModel = _dataMappingService.MapCampaignToCampaignViewModel(campaign);
                campaignViewModel.Comments = campaign.Interactions
                    .Where(i => i.InteractionType == InteractionType.Comment)
                    .Select(i => new Interaction
                    {
                        InteractionId = i.InteractionId,
                        CampaignId = i.CampaignId,
                        UserId = i.UserId,
                        InteractionType = i.InteractionType,
                        InteractionDate = i.InteractionDate,
                        Content = i.Content
                    }).ToList();

                campaignsList.Add(campaignViewModel);

            }

            // Initialize the dictionary for teacher names
            var teacherNames = new Dictionary<int, string>();
            // Fill the dictionary with teacher names
            foreach (var campaign in campaignsDb)
            {
                if (campaign.User != null && !teacherNames.ContainsKey(campaign.TeacherId))
                {
                    teacherNames[campaign.TeacherId] = campaign.User.FirstName + " " + campaign.User.LastName;
                }
            }


            // Pass the teacher names dictionary to the view
            ViewBag.TeacherNames = teacherNames;
            ViewBag.UserId = userDb.UserId;
            await UpdateViewDataUsers();
            await UpdateViewDataCommentCounts();
            return View(campaignsList);
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int campaignId)
        {
            // Fetch comments based on campaignId
            var commentsDb = await _dbContext.Interactions
                                            .Where(i => i.CampaignId == campaignId && i.InteractionType == InteractionType.Comment)
                                            .ToListAsync();
            await UpdateViewDataUsers();
            await UpdateViewDataCommentCounts();
            //TODO: 
            // Map comments to a suitable ViewModel if necessary
           // var commentsViewModel = _dataMappingService.MapInteractionsToInteractionViewModelsAsync(commentsDb);
            // Return a partial view with these comments
            return PartialView("_CommentsPartial", commentsDb);
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsCount(int campaignId)
        {
            var commentCount = await _dbContext.Interactions
                                               .CountAsync(i => i.CampaignId == campaignId && i.InteractionType == InteractionType.Comment);

            return Json(new { count = commentCount });
        }



    }
}
