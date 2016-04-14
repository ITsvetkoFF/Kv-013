using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Notes.WebApi
{
    public static class IdentityExtensions
    {
        public static string GetUserId()
        {
            var userId = ClaimsPrincipal.Current.Identity.GetUserId();
            return userId;
        }
    }
}