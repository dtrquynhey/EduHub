using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubWeb.Models;
using Microsoft.EntityFrameworkCore;

public class DataMappingService
{
    private readonly EduHubDbContext _dbContext;

    public DataMappingService(EduHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    // Custom mapping logic to map from database model to view model

    public User MapUserViewModelToUser(UserViewModel userViewModel)
    {
        return new User
        {
            Email = userViewModel.Email,
            FirstName = userViewModel.FirstName,
            LastName = userViewModel.LastName,
        };
    }

    public UserViewModel MapUserToUserViewModel(User user)
    {
        return new UserViewModel
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }

    public CampaignViewModel MapCampaignToCampaignViewModel(Campaign campaign)
    {
        return new CampaignViewModel
        {
            CampaignId = campaign.CampaignId,
            TeacherId = campaign.TeacherId,
            Title = campaign.Title,
            Description = campaign.Description,
            Content = campaign.Content,
            ImageUrl = campaign.ImageUrl,
            CreatedDate = campaign.CreatedDate,
            LastModifiedDate = campaign.LastModifiedDate,
            IsActive = campaign.IsActive,
            CampaignType = campaign.CampaignType,
            Members = campaign.Members,
            Interactions = campaign.Interactions,
            Comments = campaign.Comments,
            Engagement = campaign.Engagement,
            User = campaign.User
        };
    }
    public Campaign MapCampaignViewModelToCampaign(CampaignViewModel campaignViewModel)
    {
        return new Campaign
        {
            CampaignId = campaignViewModel.CampaignId,
            TeacherId = campaignViewModel.TeacherId,
            Title = campaignViewModel.Title,
            Description = campaignViewModel.Description,
            Content = campaignViewModel.Content,
            ImageUrl = campaignViewModel.ImageUrl,
            CreatedDate = campaignViewModel.CreatedDate,
            LastModifiedDate = campaignViewModel.LastModifiedDate,
            IsActive = campaignViewModel.IsActive,
            CampaignType = campaignViewModel.CampaignType,
            Members = campaignViewModel.Members,
            Interactions = campaignViewModel.Interactions,
            Comments = (IEnumerable<Interaction>)campaignViewModel.Comments,
            Engagement = campaignViewModel.Engagement,
            User = campaignViewModel.User
        };
    }

    public Interaction MapInteractionViewModelToInteraction(InteractionViewModel viewModel)
    {
        return new Interaction
        {
            InteractionId = viewModel.InteractionId,
            CampaignId = viewModel.CampaignId,
            UserId = viewModel.UserId,
            InteractionType = viewModel.InteractionType,
            InteractionDate = viewModel.InteractionDate,
            Content = viewModel.Content
        };
    }

    public InteractionViewModel MapInteractionToInteractionViewModel(Interaction interaction)
    {
        return new InteractionViewModel
        {
            InteractionId = interaction.InteractionId,
            CampaignId = interaction.CampaignId,
            UserId = interaction.UserId,
            InteractionType = interaction.InteractionType,
            InteractionDate = interaction.InteractionDate,
            Content = interaction.Content
        };
    }

    public async Task<List<InteractionViewModel>> MapInteractionsToInteractionViewModelsAsync(IEnumerable<Interaction> interactions)
    {
        var interactionViewModels = new List<InteractionViewModel>();

        foreach (var interaction in interactions)
        {
            var user = await _dbContext.Users.FindAsync(interaction.UserId);
            interactionViewModels.Add(new InteractionViewModel
            {
                InteractionId = interaction.InteractionId,
                CampaignId = interaction.CampaignId,
                // Instead of UserEmail, use the existing UserId to fetch and display the email in the view.
                UserId = interaction.UserId,
                InteractionType = interaction.InteractionType,
                InteractionDate = interaction.InteractionDate,
                Content = interaction.Content
                // Do not add UserEmail here
            });
        }

        return interactionViewModels;
    }

    public EngagementViewModel MapEngagementToEngagementViewModel(Engagement engagement)
    {
        return new EngagementViewModel
        {
            EngagementId = engagement.EngagementId,
            CampaignId = engagement.CampaignId,
            ViewsCount = engagement.ViewsCount,
            LikesCount = engagement.LikesCount,
            CommentsCount = engagement.CommentsCount
        };
    }

    public Engagement MapEngagementViewModelToEngagement(EngagementViewModel viewModel)
    {
        return new Engagement
        {
            EngagementId = viewModel.EngagementId,
            CampaignId = viewModel.CampaignId,
            ViewsCount = viewModel.ViewsCount,
            LikesCount = viewModel.LikesCount,
            CommentsCount = viewModel.CommentsCount
        };
    }
}
