using System.Web.Http;

using GitHubExtension.Security.WebApi;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class TestRoutesConfig
    {
        protected HttpConfiguration config;

        protected string url = "/";

        public TestRoutesConfig(string url)
        {
            config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.EnsureInitialized();
            if (url != null)
            {
                this.url += url + "/";
            }
        }
    }
}