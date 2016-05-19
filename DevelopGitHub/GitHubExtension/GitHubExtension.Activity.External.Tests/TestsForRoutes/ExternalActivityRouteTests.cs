using System.Net.Http;
using GitHubExtension.Activity.External.WebAPI.Controllers;
using MvcRouteTester;
using Xunit;
using GitHubExtension.Activity.External.WebAPI;

namespace GitHubExtension.Activity.External.Tests.TestsForRoutes
{
    public class ExternalActivityRouteTests
    {
        [Theory]
        [InlineData(1)]
        public void TestGetGitHubActivityRoute(int page)
        {
            string url = "/" + ExternalActivityRoutes.GetGitHubActivityRoute + "?page=" + page;

            ExternalActivityRouteTestConfig.GetWebApiConfiguration()
                .ShouldMap(url)
                .To<ActivityController>(HttpMethod.Get, x => x.GetGitHubActivity(page));
        }
    }
}
