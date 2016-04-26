using System.Web.Http;
using GitHubExtension.EntryPoint;

namespace GitHubExtension.Notes.Tests
{
    public class TestNoteRoutesConfig
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