using EduHubLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Interfaces
{
    public interface IUserServices
    {
        Task<bool> IsEmailExistedAsycn(string email);
        Task<User> GetUserByEmailAsycn(string email);
        Task<User> GetUserByIdAsycn(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
