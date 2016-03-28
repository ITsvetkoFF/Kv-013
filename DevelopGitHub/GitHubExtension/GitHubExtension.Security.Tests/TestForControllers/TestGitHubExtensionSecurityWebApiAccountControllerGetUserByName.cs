using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class TestGitHubExtensionSecurityWebApiAccountControllerGetUserByName
    {
        [Fact]
        public void CheckStatusCodeIfUserNotFound()
        {
            string NameToFind = "nonexistentUser";
            User user = null;
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(store);
            userManager.FindByNameAsync(NameToFind).Returns(user);

            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                Substitute.For<ISecurityContext>(), Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            //Act
            IHttpActionResult NullUser = controller.GetUserByName(NameToFind).Result;

            //Assert
            Assert.IsType<NotFoundResult>(NullUser);
        }

        [Fact]
        public void CheckStatusCodeIfUserFound()
        {
            string NameToFind = "ExsistedUser";
            User user = new User
            {
                ProviderId = 35,
                GitHubUrl = "GitHubUrl",
                UserName = "ExsistedUser",
            };
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(Substitute.For<IUserStore<User>>());
            userManager.FindByNameAsync(NameToFind).Returns(user);

            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                Substitute.For<ISecurityContext>(), Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            IHttpActionResult result = controller.GetUserByName(NameToFind).Result;

            Assert.IsType<System.Web.Http.Results.OkNegotiatedContentResult<UserReturnModel>>(result);
        }
    }
}
