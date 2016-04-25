using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

namespace GitHubExtension.Security.WebApi.Extensions.SecurityContext
{
    public static class SecurityContextExtension
    {
        public static SecurityRole GetUserRole(this ISecurityContextQuery securityContext, string roleToAssign)
        {
            SecurityRole securityRole = securityContext.SecurityRoles.FirstOrDefault(r => r.Name == roleToAssign);
            return securityRole;
        }

        public static async Task<UserRepositoryRole> GetUserRoleOnRepositoryAsync(
            this ISecurityContextQuery securityContext, 
            int repositoryId, 
            string userId)
        {
            UserRepositoryRole role = await securityContext.UserRepositoryRoles.FirstOrDefaultAsync(
                r => r.RepositoryId == repositoryId && r.UserId == userId);

            return role;
        }
    }
}