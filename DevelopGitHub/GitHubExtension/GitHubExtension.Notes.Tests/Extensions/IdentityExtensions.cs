using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GitHubExtension.Notes.Tests.Extensions
{
    public static class IdentityExtensions
    {
        public static void SetUserForController(this ApiController controller, string name, string typeOfClaim, string valueOfClaim)
        {
            var identity = new GenericIdentity(name);
            identity.AddClaim(new Claim(typeOfClaim, valueOfClaim));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            controller.User = principal;
        }
    }
}