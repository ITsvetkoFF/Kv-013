using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.Tests.Mocks;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;
using FluentAssertions;
using GitHubExtension.Activity.Internal.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerAssignRolesToUserTests
    {
        private const string roleIndex = "role";
        private const string expectedErrorForInvalidRole = "Roles '{0}' does not exists in the system";
        #region common-data

        #endregion
        public static IEnumerable<object[]> DataForNotFountResult
        {
            get
            {
                yield return new object[] 
                { 
                    new List<User>(),
                    12, 
                    15, 
                    "UserRole" 
                };
            }
        }

        public static IEnumerable<object[]> DataForInvalidModelStateResult
        {
            get
            {
                yield return new object[] 
                { 
                    new List<User>
                    {
                        new User { ProviderId = 1},
                    },
                    new List<SecurityRole>
                    {
                        new SecurityRole { Name = "Admin"}
                    },
                    1,
                    0, 
                    "BussinessAnalytics" 
                };
            }
        }

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[]
                { 
                    new List<User>
                    {
                        new User { ProviderId = 2},
                    },
                    new List<SecurityRole>
                    {
                        new SecurityRole { Name = "Admin"}
                    },
                    new User
                    {
                        ProviderId = 1
                    },
                    1, 
                    0, 
                    "Admin",
                    ActivityTypeNames.AddRole,
                    new ActivityType { Id = 2, Name = "add role" }
                };
            }
        }

        private ApplicationUserManager MockForUsers(List<User> users)
        {
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.Users.Returns(new MockForEnumerableQuery<User>(users));
            return userManager;
        }

        private IContextActivityCommand MockForActivityCommand()
        {
            var service = Substitute.For<IContextActivityCommand>();

            service.AddActivity(Arg.Any<ActivityEvent>());

            return service;
        }


        private IGetActivityTypeQuery MockForgetActivityTypeQuery(string activityTypeName, ActivityType activityType)
        {
            var query = Substitute.For<IGetActivityTypeQuery>();

            query.GetUserActivityType(activityTypeName).Returns(activityType);

            return query;
        }

        private ISecurityContext MockForContext(IEnumerable<SecurityRole> roles)
        {
            var context = Substitute.For<ISecurityContext>();
            context.SecurityRoles.Returns(new MockForDbSet<SecurityRole>(roles));
            return context;
        }

        private ApplicationUserManager MockForAddingClaim(List<User> users, User userToUpdate)
        {
            var userManager = MockForUsers(users);
            userManager.UpdateAsync(userToUpdate).Returns(IdentityResult.Success);
            userManager.CreateIdentityAsync(userToUpdate, DefaultAuthenticationTypes.ApplicationCookie).Returns(Task.FromResult(new ClaimsIdentity()));
            userManager.AddClaimAsync(userToUpdate.Id, Arg.Any<Claim>()).Returns(IdentityResult.Success);
            return userManager;
        }

        [Theory]
        [MemberData("DataForNotFountResult")]
        public void NotFoundUserTest(List<User> users, int gitHubId, int repoId, string roleToAssign)
        {
            //Arrange
            AccountController controller = new AccountController(Substitute.For<IGithubService>(), Substitute.For<IContextActivityCommand>(), Substitute.For<IGetActivityTypeQuery>(),
                Substitute.For<ISecurityContext>(), MockForUsers(users));

            //Act
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<NotFoundResult>("Because user with providerId= {0} doesn't exists in database", gitHubId);
        }

        [Theory]
        [MemberData("DataForInvalidModelStateResult")]
        public void InvalidRoleTest(List<User> users, IEnumerable<SecurityRole> roles, int gitHubId, int repoId, string roleToAssign)
        {
            //Arrenge
            AccountController controller = new AccountController(Substitute.For<IGithubService>(), Substitute.For<IContextActivityCommand>(), Substitute.For<IGetActivityTypeQuery>(),
                MockForContext(roles), MockForUsers(users));

            //Act
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<InvalidModelStateResult>("Because impossible to assign role = {0} that doesn't exist in database", roleToAssign);
        }

        [Theory]
        [MemberData("DataForInvalidModelStateResult")]
        public void ErrorMessageForInvalidRoleTest(List<User> users, IEnumerable<SecurityRole> roles, int gitHubId, int repoId, string roleToAssign)
        {
            //Arrange
            AccountController controller = new AccountController(Substitute.For<IGithubService>(), Substitute.For<IContextActivityCommand>(), Substitute.For<IGetActivityTypeQuery>(),
                MockForContext(roles), MockForUsers(users));

            //Act
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<InvalidModelStateResult>("Because impossible to assign role = {0} that doesn't exist in database", roleToAssign);
            result.Should().BeOfType<InvalidModelStateResult>().Which.ModelState[roleIndex].Errors.First().ErrorMessage.Should().Be(string.Format(expectedErrorForInvalidRole, roleToAssign));
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void OkResultTest(List<User> users, List<SecurityRole> roles, User userToUpdate, int gitHubId, int repoId, string roleToAssign, string activityTypeName, ActivityType activityType)
        {
            //Arrange
            users.Add(userToUpdate);
            AccountController controller = new AccountController(Substitute.For<IGithubService>(), MockForActivityCommand(), MockForgetActivityTypeQuery(activityTypeName, activityType),
                MockForContext(roles), MockForAddingClaim(users, userToUpdate));

            //Act
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<OkResult>();
        }
    }
}
