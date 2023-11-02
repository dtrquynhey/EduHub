using EduHubLibrary.DataAccess;
using EduHubLibrary.DataModels;
using EduHubLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Services
{
    public class UserServices : IUserServices
    {
        private readonly EduHubDbContext _dbContext;
        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsycn(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailExistedAsycn(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
