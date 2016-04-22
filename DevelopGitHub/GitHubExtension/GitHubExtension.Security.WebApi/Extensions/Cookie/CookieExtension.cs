using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using GitHubExtension.Security.WebApi.Models;

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
