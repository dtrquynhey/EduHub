using EduHubLibrary.DataAccess;
using EduHubLibrary.Interfaces;
using EduHubLibrary.Services;
using EduHubWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHubWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<CampaignsController> _logger;
        private readonly EduHubDbContext _dbContext;
        private readonly DataMappingService _dataMappingService;
        private readonly CampaignServices _campaignServices;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(
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
        
        [Authorize]
        public IActionResult Index()
        {
            List<CampaignViewModel> campaignsList = new List<CampaignViewModel>();
            var campaignsDb = _dbContext.Campaigns.ToList();
            foreach (var campaign in campaignsDb)
            {
                campaignsList.Add(_dataMappingService.MapCampaignToCampaignViewModel(campaign));
            }
            return View(campaignsList);
        }


    }
}
