using System.Web.Http;
using GitHubExtension.Security.WebApi;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class TestRoutesConfig
    {
        public TestRoutesConfig(string url)
        {
            Url = "/";
            Config = new HttpConfiguration();
            WebApiConfig.Register(Config);
            Config.EnsureInitialized();
            if (url != null)
            {
                Url += url + "/";
            }
        }

        protected HttpConfiguration Config { get; private set; }

        protected string Url { get; set; }
    }
}