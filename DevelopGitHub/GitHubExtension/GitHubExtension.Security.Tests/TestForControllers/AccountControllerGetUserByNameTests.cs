using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;
using FluentAssertions;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerGetUserByNameTests
    {
        public static IEnumerable<object[]> GetDataForNotFountResult
        {
            get
            {
                yield return new object[] { GetControllerInstance("UserName1", null), "UserName1" };
                yield return new object[] { GetControllerInstance("UserName1", null), "UserName2" };
            }
        }

        public static IEnumerable<object[]> GetDataForOkResult
        {
            get
            {
                yield return new object[] { GetControllerInstance("ExistedUser", new User { ProviderId = 4, UserName = "ExistedUser" }), "ExistedUser" };
                yield return new object[] { GetControllerInstance("FirstUser", new User { ProviderId = 5, UserName = "FirstUser" }), "FirstUser" };
            }
        }

        private static AccountController GetControllerInstance(string name, User user)
        {
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.FindByNameAsync(name).Returns(user);
            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                 Substitute.For<ISecurityContext>(), userManager);

            return controller;
        }

        [Theory]
        [MemberData("GetDataForNotFountResult")]
        public void NotFoundUserTest(AccountController controller, string nameToFind)
        {
            Task<IHttpActionResult> response = controller.GetUserByName(nameToFind);

            IHttpActionResult result = response.Result;
            result.Should().BeOfType<NotFoundResult>("Because user with name = {0} doesn't exists in database",nameToFind);
        }

        [Theory]
        [MemberData("GetDataForOkResult")]
        public void OkResultTest(AccountController controller, string nameToFind)
        {
            Task<IHttpActionResult> response = controller.GetUserByName(nameToFind);

            IHttpActionResult result = response.Result;
            result.Should().BeOfType<OkNegotiatedContentResult<UserReturnModel>>();
        }
    }
}
