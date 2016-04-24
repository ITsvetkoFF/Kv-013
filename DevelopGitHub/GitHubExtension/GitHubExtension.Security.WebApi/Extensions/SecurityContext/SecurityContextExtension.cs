using System.Linq;

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
    }
}