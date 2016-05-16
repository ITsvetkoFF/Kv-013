using System.Web.Http;
using GitHubExtension.EntryPoint;

namespace GitHubExtension.Tests.Infrastructure
{
    public class TestsRoutesConfig
    {
        public string url = "/";

        public HttpConfiguration GetRoutes()
        {
            var config = new Startup();
            var routes = config.ConfigureWebApi();
            routes.EnsureInitialized();
            return routes;
        }
    }
}
