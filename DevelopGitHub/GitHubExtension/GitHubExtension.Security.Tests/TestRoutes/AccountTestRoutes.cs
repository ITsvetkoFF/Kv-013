using Xunit;
using System.Net.Http;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.Tests.TestRoutes.TestRoutesMappers;
using GitHubExtension.Security.WebApi.Controllers;
using MvcRouteTester;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class AccountTestRoutes : TestRoutesConfig
    {
        public AccountTestRoutes()
            : base(RouteConstants.ApiAccount)
        {

        }

        [Fact]
        public void AccountGetUserTest()
        {
            url = url.ForAccountGetUser(); 

            config.ShouldMap(url)
                .To<AccountController>(HttpMethod.Get,
                x => x.GetUser("644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b"));
        }

        [Fact]
        public void AccountGetUserByNameTest()
        {
            url = url.ForAccountGetUserByName();

            config.ShouldMap(url)
                .To<AccountController>(HttpMethod.Get,
                x => x.GetUserByName("name"));
        }

        [Fact]
        public void AccountAssignRolesToUserTest()
        {
            url = url.ForAccountAssignRolesToUser();

            config.ShouldMap(url)
                .To<AccountController>(new HttpMethod("PATCH"),
                x => x.AssignRolesToUser(5, 6, null));
        }

        [Fact]
        public void AccountGetExternalLoginTest()
        {

            url = url.ForAccountGetExternalLogin();

            config.ShouldMap(url)
                .To<AccountController>(HttpMethod.Get,
                x => x.GetExternalLogin("p", "e"));
        }
    }
}
