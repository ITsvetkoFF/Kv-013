using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Extensions.Cookie
{
    public static class CookieExtensions
    {
        public static ICollection<ICollection<int>> GetCommitsRepositories(this HttpContext httpContext)
        {
            string cookieValue = HttpContext.Current.Request.Cookies["commitsRepositories"].Value;
            ICollection<ICollection<int>> _commitsRepositories = JsonConvert.DeserializeObject<ICollection<ICollection<int>>>(cookieValue);
            return _commitsRepositories;
        }

        public static void SetCommitsRepositories(this HttpContext httpContext, ICollection<ICollection<int>> commitsRepositories)
        {
                HttpContext.Current.Response.Cookies["commitsRepositories"].Value
                    = JsonConvert.SerializeObject(commitsRepositories);
        }
    }
}
