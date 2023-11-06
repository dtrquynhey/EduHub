using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubWeb.Models;
using Microsoft.EntityFrameworkCore;

public class DataMappingService
{
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
            Engagement = campaignViewModel.Engagement,
            User = campaignViewModel.User
        };
    }
}
