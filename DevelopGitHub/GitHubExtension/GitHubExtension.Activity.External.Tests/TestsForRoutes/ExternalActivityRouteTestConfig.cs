using System.Web.Http;
using GitHubExtension.EntryPoint;

namespace GitHubExtension.Activity.External.Tests.TestsForRoutes
{
    static class ExternalActivityRouteTestConfig
    {
        public static HttpConfiguration GetWebApiConfiguration()
        {
            var startup = new Startup();
            HttpConfiguration configuration = startup.ConfigureWebApi();
            configuration.EnsureInitialized();
            return configuration;
        }
    }
}
