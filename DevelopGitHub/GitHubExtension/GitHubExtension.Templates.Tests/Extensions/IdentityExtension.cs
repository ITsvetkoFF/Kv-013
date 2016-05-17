using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GitHubExtension.Templates.Tests.Extensions
{
    public static class IdentityExtension
    {
        public static void SetUserForController(this ApiController controller, string name, Claim[] claims)
        {
            var identity = new GenericIdentity(name);
            identity.AddClaims(claims);
            var principal = new GenericPrincipal(identity, new[] { "user" });
            controller.User = principal;
        }
    }
}
