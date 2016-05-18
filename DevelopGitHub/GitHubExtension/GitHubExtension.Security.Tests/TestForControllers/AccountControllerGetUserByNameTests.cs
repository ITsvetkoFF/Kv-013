using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.WebApi.Controllers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using GitHubExtention.Preferences.WebApi.Queries;
using Microsoft.AspNet.Identity;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerGetUserByNameTests
    {
        public static IEnumerable<object[]> DataForNotFountResult
        {
            get
            {
                yield return new object[] { "UserName1", null, };
            }
        }

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[] { "ExistedUser", new User { ProviderId = 4, UserName = "ExistedUser" }, };
            }
        }

        [Theory]
        [MemberData("DataForNotFountResult")]
        public void NotFoundUserTest(string nameToFind, User fakeFoundUser)
        {
            // Arrange
            AccountController controller = GetControllerInstance(nameToFind, fakeFoundUser);

            // Act
            Task<IHttpActionResult> response = controller.GetUserByName(nameToFind);

            // Assert
            IHttpActionResult result = response.Result;
            result.Should()
                .BeOfType<NotFoundResult>("Because user with name = {0} doesn't exists in database", nameToFind);
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void OkResultTest(string nameToFind, User fakeFoundUser)
        {
            // Arrange
            AccountController controller = GetControllerInstance(nameToFind, fakeFoundUser);

            // Act
            Task<IHttpActionResult> response = controller.GetUserByName(nameToFind);

            // Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<OkNegotiatedContentResult<UserReturnModel>>();
        }

        private static AccountController GetControllerInstance(string name, User user)
        {
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.FindByNameAsync(name).Returns(user);
            AccountController controller = new AccountController(
                Substitute.For<IGitHubQuery>(),
                userManager,
                Substitute.For<ISecurityContextQuery>(),
                Substitute.For<IAzureContainerQuery>());

            return controller;
        }
    }
}