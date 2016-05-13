using System.Net.Http;
using GitHubExtension.Activity.External.Tests.TestsForRoutes.Extensions;
using GitHubExtension.Activity.External.WebAPI.Controllers;
using MvcRouteTester;
using Xunit;

namespace GitHubExtension.Activity.External.Tests.TestsForRoutes
{
    public class ExternalActivityRouteTests
    {
        private string _url = "/";

        [Fact]
        public void TestGetGitHubActivityRoute()
        {
            string url = _url.ForGetGitHubActivityRoute();

            ExternalActivityRouteTestConfig.GetWebApiConfiguration()
                .ShouldMap(url)
                .To<ActivityController>(HttpMethod.Get, x => x.GetGitHubActivity(1));
        }
    }
}
