using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.DataModels
{
    public class RoleClaim
    {
        [Key]
        public int RoleClaimId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; } // Foreign key to Role

        public Role Role { get; set; } // Navigation property to Role
       
        public string ClaimValue { get; set; } = string.Empty;
    }
}
