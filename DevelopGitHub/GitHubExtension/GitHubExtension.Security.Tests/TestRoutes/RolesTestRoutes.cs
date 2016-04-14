using System.Net.Http;
using GitHubExtension.Constant;
using GitHubExtension.Security.WebApi.Controllers;
using MvcRouteTester;
using Xunit;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class RolesTestRoutes : TestRoutesConfig
    {
        public RolesTestRoutes()
            : base(RouteConstants.ApiRoles)
        {
        }

        [Fact]
        public void RolesGetTest()
        {
            this.Config.ShouldMap(this.Url).To<RolesController>(HttpMethod.Get, x => x.GetAllRoles());
        }
    }
}
