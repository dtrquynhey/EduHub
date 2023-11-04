using EduHubLibrary.DataModels;
using EduHubWeb.Models;

public class UserMappingService
{
    public User MapUserViewModelToUser(UserViewModel userViewModel)
    {
        User user = new User
        {
            // Map properties from UserViewModel to User
            Email = userViewModel.Email,
            FirstName = userViewModel.FirstName,
            LastName = userViewModel.LastName,
            // Map other properties
        };

        return user;
    }

    public UserViewModel MapUserToUserViewModel(User user)
    {
        UserViewModel userViewModel = new UserViewModel
        {
            // Map properties from User to UserViewModel
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            // Map other properties
        };

        return userViewModel;
    }
}
