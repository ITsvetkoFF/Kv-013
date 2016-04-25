using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

namespace GitHubExtension.Security.WebApi.Extensions.SecurityContext
{
    public static class SecurityContextRepositoryExtension
    {
        public static async Task<Repository> GetRepositoryByIdAsync(this ISecurityContextQuery query, int gitHubId)
        {
            return await query.Repositories.FirstOrDefaultAsync(r => r.Id == gitHubId);
        }

        public static IEnumerable<Repository> GetRepositoriesWhereUserHasRole(
            this ISecurityContextQuery securityContext, 
            string userId, 
            string roleName)
        {
            SecurityRole role = securityContext.GetUserRole(roleName);
            if (role == null)
            {
                return null;
            }

            return securityContext.Repositories.Where(
                r => r.UserRepositoryRoles.Any(ur => ur.UserId == userId && ur.SecurityRoleId == role.Id));
        }
    }
}
