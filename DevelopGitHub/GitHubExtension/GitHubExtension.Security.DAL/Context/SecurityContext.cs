using System.Data.Entity;
using GitHubExtension.Security.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.DAL.Context
{
    public class SecurityContext : IdentityDbContext<User>, ISecurityContext
    {
        public IDbSet<Repository> Repositories { get; set; }
        public IDbSet<SecurityRole> SecurityRoles { get; set; }
        public IDbSet<IdentityUserClaim> Claims { get; set; }

        public SecurityContext()
            : base("GitHubExtension")
        {}
    }
}
