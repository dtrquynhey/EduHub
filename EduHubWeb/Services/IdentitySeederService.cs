using System;
using System.Threading.Tasks;
using EduHubWeb.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace EduHubWeb.Services
{
    public class IdentitySeederService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeederService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedIdentityRolesAsync()
        {
            var roles = new[] { Roles.Admin.ToString(), Roles.Teacher.ToString(), Roles.Student.ToString() };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task SeedDefaultAdminAsync()
        {
            string email = "admin@eduhub.com";
            string password = "Admin123$";

            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(admin, password);

                await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}
