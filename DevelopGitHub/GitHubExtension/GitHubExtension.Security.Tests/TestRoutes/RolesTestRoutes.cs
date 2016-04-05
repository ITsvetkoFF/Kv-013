using GitHubExtension.Constant;
using GitHubExtension.Security.WebApi.Library.Controllers;
using MvcRouteTester;
using System.Net.Http;
using Xunit;
namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class RolesTestRoutes : TestRoutesConfig
    {
        [Fact]
        public void RolesGetTest()
        {
            url += RouteConstants.ApiRoles;

            config.ShouldMap(url)
                .To<RolesController>(HttpMethod.Get,
                x => x.GetAllRoles());
        }
    }
}
