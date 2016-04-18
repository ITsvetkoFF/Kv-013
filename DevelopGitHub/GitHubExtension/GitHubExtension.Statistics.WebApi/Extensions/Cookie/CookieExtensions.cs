using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Extensions.Cookie
{
    public static class CookieExtensions
    {
        public static ICollection<ICollection<int>> GetCommitsRepositories(this RequestContext httpContext)
        {
            string cookieValue = httpContext.HttpContext.Request.Cookies["commitsRepositories"].Value;
            ICollection<ICollection<int>> _commitsRepositories = JsonConvert.DeserializeObject<ICollection<ICollection<int>>>(cookieValue);
            return _commitsRepositories;
        }

        public static void SetCommitsRepositories(this RequestContext httpContext, ICollection<ICollection<int>> commitsRepositories)
        {
            httpContext.HttpContext.Response.Cookies["commitsRepositories"].Value
                    = JsonConvert.SerializeObject(commitsRepositories);
        }
    }
}
