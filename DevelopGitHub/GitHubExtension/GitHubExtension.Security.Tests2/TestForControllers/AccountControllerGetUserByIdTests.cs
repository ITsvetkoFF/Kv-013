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
using GitHubExtention.Preferences.WebApi;
using GitHubExtention.Preferences.WebApi.Queries;
using Microsoft.AspNet.Identity;
using NSubstitute;

using Xunit;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerGetUserByIdTests
    {
        public static IEnumerable<object[]> DataForNotFountResult
        {
            get
            {
                yield return new object[] { "1", null, };
            }
        }

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[] { "5", new User { ProviderId = 5 }, };
            }
        }

        [Theory]
        [MemberData("DataForNotFountResult")]
        public void NotFoundUserTest(string findUserById, User fakeFoundUser)
        {
            // Arrange
            AccountController controller = GetControllerInstance(findUserById, fakeFoundUser);

            // Act
            Task<IHttpActionResult> response = controller.GetUser(findUserById);

            // Assert
            IHttpActionResult result = response.Result;
            result.Should()
                .BeOfType<NotFoundResult>("Because user with id ={0} doesn't exists in database", findUserById);
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void OkResultTest(string findUserById, User fakeFoundUser)
        {
            // Arrange
            AccountController controller = GetControllerInstance(findUserById, fakeFoundUser);

            // Act
            Task<IHttpActionResult> response = controller.GetUser(findUserById);

            // Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<OkNegotiatedContentResult<UserReturnModel>>();
        }

        private static AccountController GetControllerInstance(string id, User user)
        {
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.FindByIdAsync(id).Returns(user);
            AccountController controller = new AccountController(
                Substitute.For<IGitHubQuery>(),
                userManager,
                Substitute.For<ISecurityContextQuery>(),
                Substitute.For<IAzureContainerQuery>());

            return controller;
        }
    }
}