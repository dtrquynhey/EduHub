using EduHubLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Interfaces
{
    public interface IRoleClaimService
    {
        Task<RoleClaim> GetRoleClaimByRoleIdAsync(int roleId);
        Task<IEnumerable<RoleClaim>> GetClaimsForRoleAsync(int roleClaimId);
        Task AddClaimToRoleAsync(int roleId, string claimValue);
        Task RemoveClaimFromRoleAsync(int roleClaimId);
    }
}
