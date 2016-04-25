using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using NSubstitute;

namespace GitHubExtension.Notes.Tests.Extensions
{
    public static class IdentityExtensions
    {
        public static void SetUserForController(this ApiController controller, string name, string typeOfClaim, string valueOfClaim)
        {
            var identity = Substitute.ForPartsOf<GenericIdentity>(name);
            identity.AddClaim(new Claim(typeOfClaim, valueOfClaim));
            var principal = Substitute.ForPartsOf<GenericPrincipal>(identity, new[] { "user" });
            controller.User = principal;
        }
    }
}