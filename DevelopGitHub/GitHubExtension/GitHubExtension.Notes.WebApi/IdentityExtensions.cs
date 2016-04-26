using System.Security.Claims;
using System.Security.Principal;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Notes.WebApi
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this IPrincipal user)
        {
            var claimsclaimsIdentity = user.Identity as ClaimsIdentity;
            var userId = claimsclaimsIdentity.GetUserId();
            return userId;
        }
    }
}