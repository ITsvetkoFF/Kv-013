using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;
using FluentAssertions;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerGetUserByNameTests
    {
        public static IEnumerable<object[]> DataForNotFountResult
        {
            get
            {
                yield return new object[] 
                {
                    "UserName1", 
                    null,
                };
            }
        }

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[] 
                { 
                    "ExistedUser", 
                    new User { ProviderId = 4, UserName = "ExistedUser" },
                };
            }
        }

        private static AccountController GetControllerInstance(string name, User user)
        {
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.FindByNameAsync(name).Returns(user);
            AccountController controller = new AccountController(Substitute.For<IGithubService>(), Substitute.For<IContextActivityCommand>(), Substitute.For<IGetActivityTypeQuery>(),
                 Substitute.For<ISecurityContext>(), userManager);

            return controller;
        }

        [Theory]
        [MemberData("DataForNotFountResult")]
        public void NotFoundUserTest(string nameToFind, User fakeFoundUser)
        {
            //Arrange
            AccountController controller = GetControllerInstance(nameToFind, fakeFoundUser);

            //Act
            Task<IHttpActionResult> response = controller.GetUserByName(nameToFind);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<NotFoundResult>("Because user with name = {0} doesn't exists in database", nameToFind);
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void OkResultTest(string nameToFind, User fakeFoundUser)
        {
            //Arrange
            AccountController controller = GetControllerInstance(nameToFind, fakeFoundUser);

            //Act
            Task<IHttpActionResult> response = controller.GetUserByName(nameToFind);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<OkNegotiatedContentResult<UserReturnModel>>();
        }
    }
}
