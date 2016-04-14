using System.Net.Http;
using GitHubExtension.Constant;
using GitHubExtension.Security.Tests.TestRoutes.TestRoutesMappers;
using GitHubExtension.Security.WebApi.Controllers;
using MvcRouteTester;
using Xunit;

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
            this.Url = this.Url.ForAccountGetUser();

            this.Config.ShouldMap(this.Url)
                .To<AccountController>(HttpMethod.Get, x => x.GetUser("644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b"));
        }

        [Fact]
        public void AccountGetUserByNameTest()
        {
            this.Url = this.Url.ForAccountGetUserByName();

            this.Config.ShouldMap(this.Url).To<AccountController>(HttpMethod.Get, x => x.GetUserByName("name"));
        }

        [Fact]
        public void AccountAssignRolesToUserTest()
        {
            this.Url = this.Url.ForAccountAssignRolesToUser();

            this.Config.ShouldMap(this.Url)
                .To<AccountController>(new HttpMethod("PATCH"), x => x.AssignRolesToUser(5, 6, null));
        }

        [Fact]
        public void AccountGetExternalLoginTest()
        {
            this.Url = this.Url.ForAccountGetExternalLogin();

            this.Config.ShouldMap(this.Url).To<AccountController>(HttpMethod.Get, x => x.GetExternalLogin("p", "e"));
        }
    }
}
