using System.Security.Claims;
using System.Security.Principal;

using GitHubExtension.Infrastructure.Constants;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Infrastructure.Extensions.Identity
{
    public static class IdentityExtensions
    {
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