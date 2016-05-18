using System.Net.Http;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Security.Test.TestRoutes.TestRoutesMappers;
using GitHubExtension.Security.WebApi.Controllers;
using MvcRouteTester;
using Xunit;

namespace GitHubExtension.Security.Test.TestRoutes
{
    public class AccountTestRoutes : TestRoutesConfig
    {
        public AccountTestRoutes()
            : base(RouteConstants.ApiAccount)
        {
        }

        [Fact]
        public void AccountGetExternalLoginTest()
        {
            Url = Url.ForAccountGetExternalLogin();

            Config.ShouldMap(Url).To<AccountController>(HttpMethod.Get, x => x.GetExternalLogin("p"));
        }

        [Fact]
        public void AccountGetUserByNameTest()
        {
            Url = Url.ForAccountGetUserByName();

            Config.ShouldMap(Url).To<AccountController>(HttpMethod.Get, x => x.GetUserByName("name"));
        }

        [Fact]
        public void AccountGetUserTest()
        {
            Url = Url.ForAccountGetUser();

            Config.ShouldMap(Url)
                .To<AccountController>(HttpMethod.Get, x => x.GetUser("644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b"));
        }

        [Fact]
        public void RepositoryAssignRolesToUserTest()
        {
            Url = Url.ForAccountAssignRolesToUser();

            Config.ShouldMap(Url)
                .To<RepositoryController>(new HttpMethod("PATCH"), x => x.AssignRolesToUser(5, 6, null));
        }
    }
}