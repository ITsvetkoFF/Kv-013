using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using GitHubExtension.Security.DAL.Infrastructure;
using Microsoft.AspNet.Identity;
using GitHubExtension.Security.StorageModels.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.WebApi.Library.Services;
using GitHubExtension.Security.DAL.Interfaces;
using System.Web.Http;
using System.Web.Http.Results;
using GitHubExtension.Security.WebApi.Library.Controllers;



namespace GitHubExtension.Security.Tests.Te
{
    public class TestGitHubExtensionSecurityWebApiAccountController
    {
        [Fact]
        public void GetUserByIdCheckStatusCodeIfNotFound()
        {
            //Arrange
            string idToFind ="34";
            User user = null;
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(store);
            userManager.FindByIdAsync(idToFind).Returns(user);

            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                Substitute.For<ISecurityContext>(), Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            //Act
            IHttpActionResult NullUser = controller.GetUser(idToFind).Result;

            //Assert
            Assert.IsType<NotFoundResult>(NullUser);
        }

        [Fact]
        public void GetUserByIdCheckStatusCodeIfUserFound()
        {
            string idToFind = "35";
            User user = new User
            {
                ProviderId = 35,
                GitHubUrl = "GitHubUrl"
            };
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(store);
            userManager.FindByIdAsync(idToFind).Returns(user);

            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                Substitute.For<ISecurityContext>(), Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            IHttpActionResult result = controller.GetUser(idToFind).Result;

            Assert.IsType<System.Web.Http.Results.OkNegotiatedContentResult<UserReturnModel>>(result);
        }


        
    }
}
