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

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerAssignRolesToUserTests
    {
        private const string roleIndex = "role";
        private const string expectedErrorForInvalidRole = "Roles '{0}' does not exists in the system";
        public static IEnumerable<object[]> GetDataForNotFountResult
        {
            get
            {
                yield return new object[] { GetControllerInstance(2, "UserRole"), 12, 15, "UserRole" };
            }
        }

        public static IEnumerable<object[]> GetDataForInvalidModelStateResult
        {
            get
            {
                yield return new object[] { GetControllerInstance(2, "BussinessAnalytics"), 1, 0, "BussinessAnalytics" };
            }
        }

        public static IEnumerable<object[]> GetDataForOkResult
        {
            get
            {
                yield return new object[] { GetControllerInstance(2, "Admin"), 1, 0, "Admin" };
            }
        }

        private static object GetControllerInstance(int providerId, string role)
        {
            User userToUpdate = new User
            {
                Id = "111",
                ProviderId = 1,
                UserRepositoryRoles = new List<UserRepositoryRole>
               {
                   new UserRepositoryRole { Id =2 , },
                   new UserRepositoryRole { Id =3 , },
               }
            };
            List<User> users = new List<User>
            {
                new User{ProviderId = 4,},
                new User{ProviderId = providerId},
                userToUpdate,
            };
            IEnumerable<SecurityRole> roles = new List<SecurityRole>
            {
                new SecurityRole{ Id = 1, Name = "Admin" },
                new SecurityRole{ Id = 2, Name = "Developer" },
                new SecurityRole{ Id = 3, Name = "Reviewer" }
            };
            UserRepositoryRole repositoryRoleToFind = new UserRepositoryRole { Id = 12 };
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.Users.Returns(new MockForEnumerableQuery<User>(users));
            userManager.UpdateAsync(userToUpdate).Returns(IdentityResult.Success);
            userManager.CreateIdentityAsync(userToUpdate, DefaultAuthenticationTypes.ApplicationCookie).Returns(Task.FromResult(new ClaimsIdentity()));
            userManager.AddClaimAsync(userToUpdate.Id, Arg.Any<Claim>()).Returns(IdentityResult.Success);
            var context = Substitute.For<ISecurityContext>();
            context.SecurityRoles.Returns(new MockForDbSet<SecurityRole>(roles));

            return new AccountController(Substitute.For<IGithubService>(),
                context, userManager);
        }

        [Theory]
        [MemberData("GetDataForNotFountResult")]
        public void NotFoundUserTest(AccountController controller, int gitHubId, int repoId, string roleToAssign)
        {
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            IHttpActionResult result = response.Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [MemberData("GetDataForInvalidModelStateResult")]
        public void InvalidRoleTest(AccountController controller, int gitHubId, int repoId, string roleToAssign)
        {
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            IHttpActionResult result = response.Result;
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Theory]
        [MemberData("GetDataForInvalidModelStateResult")]
        public void ErrorMessageForInvalidRoleTest(AccountController controller, int gitHubId, int repoId, string roleToAssign)
        {
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            IHttpActionResult result = response.Result;
            InvalidModelStateResult modelStateResult = Assert.IsType<InvalidModelStateResult>(result);
            Assert.Equal(string.Format(expectedErrorForInvalidRole, roleToAssign), modelStateResult.ModelState[roleIndex].Errors.First().ErrorMessage);
        }

        [Theory]
        [MemberData("GetDataForOkResult")]
        public void OkResultTest(AccountController controller, int gitHubId, int repoId, string roleToAssign)
        {
            Task<IHttpActionResult> response = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign);

            IHttpActionResult result = response.Result;
            Assert.IsType<OkResult>(result);
        }
    }
}
