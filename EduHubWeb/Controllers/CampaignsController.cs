using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubLibrary.Interfaces;
using EduHubLibrary.Services;
using EduHubWeb.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduHubWeb.Controllers
{
    public class CampaignsController : BaseController
    {
        private readonly ILogger<CampaignsController> _logger;
        private readonly DataMappingService _dataMappingService;
        private readonly CampaignServices _campaignServices;
        private readonly InteractionServices _interactionServices;

        public CampaignsController(
            ILogger<CampaignsController> logger,
            EduHubDbContext dbContext,
            DataMappingService dataMappingService,
            CampaignServices campaignServices,
            UserManager<IdentityUser> userManager,
            InteractionServices interactionServices) : base(dbContext, userManager)
        {
            _logger = logger;
            _dataMappingService = dataMappingService;
            _campaignServices = campaignServices;
            _interactionServices = interactionServices;
        }

        // GET: CampaigsnController
        public async Task<ActionResult> Index()
        {
            List<CampaignViewModel> campaignsView = new List<CampaignViewModel>();
            var campaignsDb = await _dbContext.Campaigns
                                               .Include(c => c.Engagement) // Ensure you include Engagement data
                                               .ToListAsync(); 
            foreach (var campaign in campaignsDb)
            {
                campaignsView.Add(_dataMappingService.MapCampaignToCampaignViewModel(campaign));
            }
            await SetCurrentUserId();

            return View(campaignsView);
        }

        // GET: CampaigsnController/Details/5
        public async Task<ActionResult> Detail(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            User userDb = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            var userLikeStatus = await _interactionServices.GetUserLikeStatus(userDb.UserId);

            ViewBag.UserLikeStatus = userLikeStatus;
            await SetCurrentUserId();

            Campaign campaign = _dbContext.Campaigns
            .FirstOrDefault(c => c.CampaignId == id);

            if (campaign == null)
            {
                return NotFound(); // Handle the case where the campaign with the specified ID is not found.
            }

            return View("Detail", _dataMappingService.MapCampaignToCampaignViewModel(campaign));
        }

        // GET: CampaigsnController/Create

        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            var campaignViewModel = new CampaignViewModel(); // Initialize with default values if needed
            return View(campaignViewModel);
        }

        // POST: CampaigsnController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CampaignViewModel campaignViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var teacher = await _userManager.FindByIdAsync(teacherId);
                    if (teacher.Email != null)
                    {
                        User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == teacher.Email);
                        if (user != null)
                        {
                            campaignViewModel.CreatedDate = DateTime.Now;
                            campaignViewModel.TeacherId = user.UserId;
                        }
                    }
                    //await _campaignServices.CreateCampaignAsync(campaignDb);

                    var engagement = await _dbContext.Engagements.FirstOrDefaultAsync(e => e.CampaignId == campaignViewModel.CampaignId);
                    if (engagement == null)
                    {
                        engagement = new Engagement { CampaignId = campaignViewModel.CampaignId, LikesCount = 0, CommentsCount = 0, ViewsCount = 0};
                        _dbContext.Engagements.Add(engagement);
                        campaignViewModel.Engagement = engagement;
                    }
                    var campaignDb = _dataMappingService.MapCampaignViewModelToCampaign(campaignViewModel);

                    _dbContext.Campaigns.Add(campaignDb);

                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                    // If ModelState is not valid, return to the Create view with the current model to show validation errors
                    return View(campaignViewModel);
            }
            catch (Exception)
            {
                // Handle exceptions, if necessary, and log the error for debugging
                return View();
            }
        }


        public async Task<ActionResult> Edit(int id)
        {
            var campaign = await _dbContext.Campaigns // AsNoTracking is optional here since we're not updating in this action
                .FirstOrDefaultAsync(c => c.CampaignId == id);

            if (campaign == null)
            {
                _logger.LogError($"Campaign with id {id} not found.");
                return NotFound(); // Handle the case where the campaign with the specified ID is not found.
            }

            return View(_dataMappingService.MapCampaignToCampaignViewModel(campaign));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CampaignViewModel campaignViewModel)
        {
            if (!ModelState.IsValid)
            {
                // If ModelState is not valid, return to the Edit view with the current model to show validation errors
                return View(campaignViewModel);
            }

            var campaignDb = await _dbContext.Campaigns.AsNoTracking().FirstOrDefaultAsync(c => c.CampaignId == id);
            if (campaignDb == null)
            {
                _logger.LogError($"Campaign with id {id} not found.");
                return NotFound();
            }

            // Mapping updates from the ViewModel to the DataModel
            campaignDb = _dataMappingService.MapCampaignViewModelToCampaign(campaignViewModel);

            // It's important to set the ID from the path parameter to the entity
            // so that EF knows which entity to update
            campaignDb.CampaignId = id;

            // Mark the entity as modified
            _dbContext.Update(campaignDb);

            try
            {
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Campaign edited successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                // Handle exceptions related to database updates
                _logger.LogError(ex, "An error occurred while updating the campaign.");
                return View(campaignViewModel);
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions
                _logger.LogError(ex, "An error occurred while editing the campaign.");
                return View(campaignViewModel);
            }
        }

        // GET: CampaigsnController/Delete/5
        public ActionResult Delete(int id)
        {
            var campaign = _dbContext.Campaigns.FirstOrDefault(c => c.CampaignId == id);

            if (campaign == null)
            {
                _logger.LogError($"Campaign with id {id} not found.");
                return NotFound(); // Handle the case where the campaign with the specified ID is not found.
            }

            return View(_dataMappingService.MapCampaignToCampaignViewModel(campaign));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var campaign = await _dbContext.Campaigns.FirstOrDefaultAsync(c => c.CampaignId == id);
                    
                    _dbContext.Campaigns.Remove(campaign);
                    await _dbContext.SaveChangesAsync();

                    _logger.LogInformation("Campaign deleted successfully.");
                    return RedirectToAction("Index");
                }
                else
                    // If ModelState is not valid, return to the Create view with the current model to show validation errors
                    return View();
            }
            catch (DbUpdateException ex)
            {
                // Handle exceptions related to database updates
                _logger.LogError(ex, "An error occurred while deleting the campaign.");
                return View();
            }
            catch (Exception ex)
            {
                // Handle exceptions, if necessary, and log the error for debugging
                _logger.LogError(ex, "An error occurred while deleting a campaign.");
                return View();
            }
        }


    }
}
