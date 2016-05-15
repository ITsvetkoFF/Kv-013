using System.Security.Claims;
using System.Security.Principal;
using GitHubExtension.Infrastructure.Constants;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.Extensions.Identity
{
    public static class IdentityExtensions
    {
        private const string CurrentProjectName = "CurrentProjectName";
        private const string CurrentProjectId = "CurrentProjectId";

        public static string GetCurrentProjectName(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirstValue(CurrentProjectName);
        }

        public static string GetCurrentProjectId(this IPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirstValue(CurrentProjectId);
        }

        public static Claim GetExternalAccessTokenClaim(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst(ClaimConstants.ExternalAccessToken);
        }

        public static string GetExternalAccessToken(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirstValue(ClaimConstants.ExternalAccessToken);
        }
    }
}
