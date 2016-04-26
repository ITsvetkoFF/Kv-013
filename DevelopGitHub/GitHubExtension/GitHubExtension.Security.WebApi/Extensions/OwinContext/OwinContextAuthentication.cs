using System.Web;
using System.Web.Routing;

using Microsoft.Owin.Security;

namespace GitHubExtension.Security.WebApi.Extensions.OwinContext
{
    public static class OwinContextAuthentication
    {
        public static IAuthenticationManager Authentication(this RequestContext requestContext)
        {
            IAuthenticationManager authenticationManager = requestContext.HttpContext.GetOwinContext().Authentication;
            return authenticationManager;
        }
    }
}