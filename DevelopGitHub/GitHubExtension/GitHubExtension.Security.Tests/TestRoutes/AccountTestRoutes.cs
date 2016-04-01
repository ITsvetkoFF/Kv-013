using System;
using Xunit;
using MvcRouteTester;
using GitHubExtension.Security.WebApi.Library.Controllers;
using System.Net.Http;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Constant;
using System.Web.Http;
using System.Text.RegularExpressions;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class AccountTestRoutes : TestRoutesConfig
    {
        [Fact]
        public void AccountGetUserTest()
        {
            url = "/" + RouteConstant.apiAccount +
                "/" + Regex.Replace(
                RouteConstant.getUser,
                RouteConstant.id_guid,
                "644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b");

            config.ShouldMap(url)
                .To<AccountController>(HttpMethod.Get,
                x => x.GetUser("644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b"));
        }

        [Fact]
        public void AccountGetUserByNameTest()
        {
            url = "/" + RouteConstant.apiAccount +
                "/" + Regex.Replace(
                RouteConstant.getUserByName,
                RouteConstant.userName,
                "name");

            config.ShouldMap(url)
                .To<AccountController>(HttpMethod.Get,
                x => x.GetUserByName("name"));
        }

        [Fact]
        public void AccountAssignRolesToUserTest()
        {
            url = "/" + RouteConstant.apiAccount +
                "/" + Regex.Replace(
                Regex.Replace(
                    RouteConstant.assignRolesToUser,
                    RouteConstant.repositoryId,
                    "5"),
                RouteConstant.gitHubId,
                "6");

            config.ShouldMap(url)
                .To<AccountController>(new HttpMethod("PATCH"),
                x => x.AssignRolesToUser(5, 6, null));
        }

        [Fact]
        public void AccountGetExternalLoginTest()
        {
            url = "/" + RouteConstant.apiAccount +
                "/" + RouteConstant.getExternalLogin +
                "?provider=p&error=e";

            config.ShouldMap(url)
                .To<AccountController>(HttpMethod.Get,
                x => x.GetExternalLogin("p", "e"));
        }
    }
}
