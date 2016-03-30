using System.Collections.Generic;
using NSubstitute;
using Xunit;
using GitHubExtension.Security.DAL.Infrastructure;
using Microsoft.AspNet.Identity;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.WebApi.Library.Services;
using GitHubExtension.Security.DAL.Interfaces;
using System.Web.Http;
using System.Web.Http.Results;
using GitHubExtension.Security.WebApi.Library.Controllers;
using System.Data.Entity.Infrastructure;
using GitHubExtension.Security.Tests.Mocks;
using System.Threading.Tasks;



namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerGetUserByIdTests
    {
        public static IEnumerable<object[]> GetDataForNotFountResult
        {
            get
            {
                yield return new object[] { GetControllerInstance("1", null), "1" };
                yield return new object[] { GetControllerInstance("2", null), "2" };
            }
        }

        public static IEnumerable<object[]> GetDataForOkResult
        {
            get
            {
                yield return new object[] { GetControllerInstance("4", new User { ProviderId = 4 }), "4" }; 
                yield return new object[] { GetControllerInstance("5", new User { ProviderId = 5 }), "5" };
            }
        }

        private static AccountController GetControllerInstance(string id, User user)
        {
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.FindByIdAsync(id).Returns(user);
            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                 Substitute.For<ISecurityContext>(), userManager);

            return controller;
        }
         
        [Theory]
        [MemberData("GetDataForNotFountResult")]
        public void NotFoundUserTest(AccountController controller,string findUserById)
        {
            Task<IHttpActionResult> NullUser = controller.GetUser(findUserById);

            IHttpActionResult result = NullUser.Result;
            Assert.IsType<NotFoundResult>(result);
        }


        [Theory]
        [MemberData("GetDataForOkResult")]
        public void OkResultTest(AccountController controller, string findUserById)
        {
            Task<IHttpActionResult> response = controller.GetUser(findUserById);

            IHttpActionResult result = response.Result;
            Assert.IsType<OkNegotiatedContentResult<UserReturnModel>>(result);
        }

    }
}
