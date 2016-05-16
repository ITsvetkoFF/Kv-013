using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.Tests.Extensions;
using GitHubExtension.Security.Tests.Mocks;
using GitHubExtension.Security.WebApi.Controllers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Interfaces;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class UserControllerGetAllUsersByNameTests
    {
        private const string UserName = "UserName";
        private const string CurrentUserName = "CurrentUserName";

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[]
                {
                    new List<User>()
                    {
                        new User
                        {
                            UserName = "UserName"
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> DataForNotFoundResult
        {
            get
            {
                yield return new object[]
                {
                    new List<User>()
                    {
                        new User
                        {
                            UserName = "UserNotFound"
                        }
                    }
                };
            }
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void ShouldReturnOkResultWhenUsersExist(IEnumerable<User> users)
        {
            // Arrange
            var userController = GetControllerInstance(users);

            // Act
            var result = userController.GetAllUsersByName(UserName);

            // Assert
            result.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<UserReturnModel>>>();
        }

        [Theory]
        [MemberData("DataForNotFoundResult")]
        public void ShouldReturnNotFoundResultWhenUsersNotExist(IEnumerable<User> users)
        {
            // Arrange
            var userController = GetControllerInstance(users);

            // Act
            var result = userController.GetAllUsersByName(UserName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        private static ISecurityContextQuery MockForSecurityContextQuery(IEnumerable<User> users)
        {
            var securityContextQuery = Substitute.For<ISecurityContextQuery>();
            securityContextQuery.Users.Returns(new MockForDbSet<User>(users));
            return securityContextQuery;
        }

        private static UserController GetControllerInstance(IEnumerable<User> users)
        {
            var securityContextQuery = MockForSecurityContextQuery(users);
            var userController = new UserController(securityContextQuery)
            {
                User = Substitute.For<IPrincipal>().SetUserForController(CurrentUserName)
            };
            return userController;
        }
    }
}
