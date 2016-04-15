using System.Web.Http;
using GitHubExtension.Security.WebApi;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class TestRoutesConfig
    {
        private string url = "/";

        public TestRoutesConfig(string url)
        {
            this.Config = new HttpConfiguration();
            WebApiConfig.Register(this.Config);
            this.Config.EnsureInitialized();
            if (url != null)
            {
                this.Url += url + "/";
            }
        }

        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        public HttpConfiguration Config { get; private set; }
    }
}
