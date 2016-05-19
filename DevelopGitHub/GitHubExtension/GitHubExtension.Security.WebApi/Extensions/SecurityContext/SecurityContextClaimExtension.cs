using System.Linq;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.WebApi.Extensions.SecurityContext
{
    public static class SecurityContextClaimExtension
    {
        public static IdentityUserClaim GetCurrentProjectIdClaim(this ISecurityContextQuery securityContextQuery, string userId)
        {
            var claim =
                securityContextQuery.Claims.FirstOrDefault(
                    c => c.ClaimType == ClaimConstants.CurrentProjectId && c.UserId == userId);
            return claim;
        }

        public static IdentityUserClaim GetCurrentProjectNameClaim(this ISecurityContextQuery securityContextQuery, string userId)
        {
            var claim =
                securityContextQuery.Claims.FirstOrDefault(
                    c => c.ClaimType == ClaimConstants.CurrentProjectName && c.UserId == userId);
            return claim;
        }
    }
}
