using System.Linq;

using GitHubExtension.Security.DAL.Identity;

namespace GitHubExtension.Security.WebApi.Queries.Interfaces
{
    public interface ISecurityContextQuery
    {
        IQueryable<SecurityRole> SecurityRoles { get; }

        IQueryable<UserRepositoryRole> UserRepositoryRoles { get; }

        IQueryable<Repository> Repositories { get; }
    }
}