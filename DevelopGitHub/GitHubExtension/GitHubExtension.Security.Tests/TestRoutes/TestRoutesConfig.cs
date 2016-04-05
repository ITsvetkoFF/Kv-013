using GitHubExtension.Security.WebApi.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class TestRoutesConfig : IClassFixture<SlashInitializer>
    {
        protected HttpConfiguration config;
        protected string url;

        public TestRoutesConfig()
        {
            config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.EnsureInitialized();
            url = SlashInitializer.Slash;
        }
    }
}
