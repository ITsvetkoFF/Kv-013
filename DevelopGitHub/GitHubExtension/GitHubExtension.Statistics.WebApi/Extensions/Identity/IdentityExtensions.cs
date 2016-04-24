using System.Security.Claims;
using System.Security.Principal;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Statistics.WebApi.Extensions.Identity
{
    public static class IdentityExtensions
    {
        public static Claim GetClaim(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst("ExternalAccessToken");
        }

        public static string GetExternalAccessToken(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirstValue("ExternalAccessToken");
        }
    }
}