using System.Collections.Generic;
using System.Web.Routing;

using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Extensions.Cookie
{
    public static class CookieExtensions
    {
        public static ICollection<ICollection<int>> GetCommitsRepositories(this RequestContext httpContext)
        {
            string cookieValue = httpContext.HttpContext.Request.Cookies["commitsRepositories"].Value;
            ICollection<ICollection<int>> commitsRepositories =
                JsonConvert.DeserializeObject<ICollection<ICollection<int>>>(cookieValue);
            return commitsRepositories;
        }

        public static void SetCommitsRepositories(
            this RequestContext httpContext, 
            ICollection<ICollection<int>> commitsRepositories)
        {
            httpContext.HttpContext.Response.Cookies["commitsRepositories"].Value =
                JsonConvert.SerializeObject(commitsRepositories);
        }
    }
}