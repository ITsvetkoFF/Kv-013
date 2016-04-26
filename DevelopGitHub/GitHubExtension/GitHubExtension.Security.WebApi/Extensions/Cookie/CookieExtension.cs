using System.Web.Routing;

namespace GitHubExtension.Security.WebApi.Extensions.Cookie
{
    public static class CookieExtensions
    {
        public static void SetUserCookie(this RequestContext httpContext, string userName)
        {
            httpContext.HttpContext.Response.Cookies["userName"].Value = userName;
        }
    }
}