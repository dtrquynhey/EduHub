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
    public class NewsfeedController : Controller
    {
        private readonly EduHubDbContext _dbContext;
        private readonly DataMappingService _dataMappingService;
        private readonly InteractionServices _interactionServices;
        private readonly UserManager<IdentityUser> _userManager;


        public NewsfeedController(EduHubDbContext dbContext,
            DataMappingService dataMappingService,
            InteractionServices interactionServices,
            UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _dataMappingService = dataMappingService;
            _interactionServices = interactionServices;
            _userManager = userManager;
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

                int commentsCount = campaign.Interactions.Count(i => i.InteractionType == InteractionType.Comment);
                var engagementDb = await _dbContext.Engagements.FirstOrDefaultAsync(e => e.CampaignId == campaign.CampaignId);
                if (engagementDb != null)
                {
                    var engagementViewModel = _dataMappingService.MapEngagementToEngagementViewModel(engagementDb);

                    engagementViewModel.CommentsCount = commentsCount;
                    campaignViewModel.Engagement = engagementDb;
                }
                else
                {
                    campaignViewModel.Engagement = new Engagement
                    {
                        CampaignId = campaign.CampaignId,
                        ViewsCount = 0,
                        LikesCount = 0,
                        CommentsCount = commentsCount
                    };
                }
            }

            var userIds = campaignsDb.SelectMany(c => c.Interactions.Select(i => i.UserId)).Distinct();
            var userDictionary = await _dbContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionaryAsync(u => u.UserId, u => u.FirstName + u.LastName);

            // Pass this dictionary to the view via ViewBag or ViewData
            ViewData["Names"] = userDictionary;


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

            return View(campaignsList);
        }
    }
}
