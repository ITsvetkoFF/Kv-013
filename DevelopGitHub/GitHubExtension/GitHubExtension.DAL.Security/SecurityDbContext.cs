using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.Models.StorageModels.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Domain
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser>, ISecurityDbContext
    {
        public SecurityDbContext()
            : base("SecurityDataBase", throwIfV1Schema: false) { }

        public static SecurityDbContext Create()
        {
            return new SecurityDbContext();
        }
    }
}
