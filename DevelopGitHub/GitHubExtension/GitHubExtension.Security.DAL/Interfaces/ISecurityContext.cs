using System.Data.Entity;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Interfaces
{
    public interface ISecurityContext
    {
        IDbSet<Repository> Repositories { get; set; }
        IDbSet<SecurityRole> SecurityRoles { get; set; }
        IDbSet<IdentityUserClaim> Claims { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
    }
}
