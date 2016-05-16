using System.Net.Http;
using GitHubExtension.Security.Tests2.TestRoutes.TestRoutesMappers;
using GitHubExtension.Security.WebApi.Controllers;
using MvcRouteTester;
using Xunit;

namespace GitHubExtension.Security.Tests2.TestRoutes
{
    public class UserTestRoutes : TestRoutesConfig
    {
        public UserTestRoutes() : base(null)
        {
        }

        [Fact]
        public void GetAllUsersByNameRouteTest()
        {
            Url = Url.ForGetAllUsersByName();
            Config.ShouldMap(Url).To<UserController>(HttpMethod.Get, x => x.GetAllUsersByName("test"));
        }
    }
}
