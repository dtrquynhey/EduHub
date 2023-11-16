using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EduHubWeb.Services
{
    public class PopulateRolesFilter : IAsyncActionFilter
    {
        private readonly UserManager<IdentityUser> _userManager;

        public PopulateRolesFilter(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(context.HttpContext.User);
                var roles = await _userManager.GetRolesAsync(user);
                context.HttpContext.Items["Roles"] = roles;
            }

            // Call the action
            await next();
        }
    }
}
