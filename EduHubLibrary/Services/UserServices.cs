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

        public UserServices(EduHubDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task CreateUserAsync(User user)
        {
            if (user != null)
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            // TODO: Implement customer Exception class
            throw new Exception();
        }

        public async Task DeleteUserAsync(int id)
        {
            Task<User> user = GetUserByIdAsycn(id);
            if (user != null)
            {
                _dbContext.Users.Remove(await user);
            }
            throw new Exception();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
            throw new Exception();
        }

        public async Task<User> GetUserByEmailAsycn(string email)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return user;
            }
            throw new Exception();
        }

        public async Task<User> GetUserByIdAsycn(int id)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
            {
                return user;
            }
            throw new Exception();
        }

        public async Task<bool> IsEmailExistedAsycn(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);

            throw new Exception();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user != null)
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            throw new Exception();
        }
    }
}
