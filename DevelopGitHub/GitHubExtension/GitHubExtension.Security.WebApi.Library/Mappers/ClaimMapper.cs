using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Security.WebApi.Library.Mappers
{
    public static class ClaimMapper
    {
        public static string GetExternalAccessToken(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirstValue("ExternalAccessToken");
        }

        public static Claim GetClaim(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst("ExternalAccessToken");
        }
    }
}
