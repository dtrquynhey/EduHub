using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubLibrary.Services;
using EduHubWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduHubWeb.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ILogger<CampaignsController> _logger;
        private readonly EduHubDbContext _dbContext;
        private readonly DataMappingService _dataMappingService;
        private readonly CampaignServices _campaignServices;
        private readonly UserManager<IdentityUser> _userManager;

        public CampaignsController(
            ILogger<CampaignsController> logger, 
            EduHubDbContext dbContext, 
            DataMappingService dataMappingService, 
            CampaignServices campaignServices,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _dataMappingService = dataMappingService;
            _campaignServices = campaignServices;
            _userManager = userManager;
        }

        // GET: CampaigsnController
        public ActionResult Index()
        {
            List<CampaignViewModel> campaignsView = new List<CampaignViewModel>();
            var campaignsDb = _dbContext.Campaigns.ToList();
            foreach (var campaign in campaignsDb)
            {
                campaignsView.Add(_dataMappingService.MapCampaignToCampaignViewModel(campaign));
            }
            return View(campaignsView);
        }

        // GET: CampaigsnController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public async Task<ActionResult> CreateAsync(CampaignViewModel campaignViewModel)
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
                            campaignViewModel.TeacherId = user.UserId;
                        }
                    }
                    var campaignDb = _dataMappingService.MapCampaignViewModelToCampaign(campaignViewModel);
                    //await _campaignServices.CreateCampaignAsync(campaignDb);

                    _dbContext.Campaigns.Add(campaignDb);
                    await _dbContext.SaveChangesAsync();

                    _logger.LogInformation("Campaign created successfully.");
                    return RedirectToAction("Index");
                }
                else
                // If ModelState is not valid, return to the Create view with the current model to show validation errors
                    return View(campaignViewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions, if necessary, and log the error for debugging
                _logger.LogError(ex, "An error occurred while creating a campaign.");
                return View();
            }
        }


        // GET: CampaigsnController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CampaigsnController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CampaigsnController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CampaigsnController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
