using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Domain.Interfaces
{
    public interface ISecurityContext
    {
        IDbSet<Repository> Repositories { get; set; }
        IDbSet<SecurityRole> SecurityRoles { get; set; }
        IDbSet<IdentityUserClaim> Claims { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        IDbSet<Client> Clients { get; set; }
    }
}
