using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Security.WebApi;
using Xunit;

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
                this.url += url + "/";
        }
    }
}
