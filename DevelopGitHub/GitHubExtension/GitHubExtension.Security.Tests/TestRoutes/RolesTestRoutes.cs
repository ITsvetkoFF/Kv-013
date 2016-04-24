using System.Net.Http;

using GitHubExtension.Infrastructure.Constants;
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
            config.ShouldMap(url).To<RolesController>(HttpMethod.Get, x => x.GetAllRoles());
        }
    }
}