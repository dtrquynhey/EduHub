using EduHubLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Interfaces
{
    public interface IRoleServices
    {
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
