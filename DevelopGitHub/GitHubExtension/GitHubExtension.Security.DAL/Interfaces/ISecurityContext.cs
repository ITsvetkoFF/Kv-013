using System.Data.Entity;

using GitHubExtension.Security.DAL.Identity;

using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Interfaces
{
    public interface ISecurityContext
    {
        IDbSet<IdentityUserClaim> Claims { get; set; }

        IDbSet<Repository> Repositories { get; set; }

        IDbSet<IdentityRole> Roles { get; set; }

        IDbSet<SecurityRole> SecurityRoles { get; set; }

        IDbSet<UserRepositoryRole> UserRepository { get; set; }

        IDbSet<User> Users { get; set; }
    }
}