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
            url = "/" + RouteConstant.apiRoles;

            config.ShouldMap(url)
                .To<RolesController>(HttpMethod.Get,
                x => x.GetAllRoles());
        }
    }
}
