using System.Linq;

using GitHubExtension.Security.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.WebApi.Queries.Interfaces
{
    public interface ISecurityContextQuery
    {
        IQueryable<SecurityRole> SecurityRoles { get; }

        IQueryable<UserRepositoryRole> UserRepositoryRoles { get; }

        IQueryable<Repository> Repositories { get; }

        IQueryable<User> Users { get; }

        IQueryable<IdentityUserClaim> Claims { get; }  
    }
}