using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.Tests.Mocks;
using GitHubExtension.Security.WebApi.Library.Controllers;
using GitHubExtension.Security.WebApi.Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Xunit;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class TestGitHubExtensionSecurityWebApiAccountControllerAssignRolesToUser
    {
        [Fact]
        public void CheckUserNotFound()
        {
            int gitHubId = 12;
            int repoId = 0;
            string roleToAssign = "role";
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(store);
            List<User> users = new List<User>{
                new User{ProviderId = 1,},
                new User{ProviderId =2, },
                new User{ProviderId=3,},
            };

            userManager.Users.Returns(new MockForEnumerableQuery<User>(users));

            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                Substitute.For<ISecurityContext>(), Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            IHttpActionResult nullUser = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign).Result;

            Assert.IsType<NotFoundResult>(nullUser);
        }

        [Fact]
        public void CheckStatusCodeIfRoleNotExists()
        {
            int gitHubId = 1;
            int repoId = 0;
            string roleToAssign = "BussinessAnalytics";
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(store);
            List<User> users = new List<User>{
                new User{ProviderId = 1,},
                new User{ProviderId =2, },
                new User{ProviderId=3,},
            };

            userManager.Users.Returns(new MockForEnumerableQuery<User>(users));
            var context = Substitute.For<ISecurityContext>();
            IEnumerable<SecurityRole> roles = new List<SecurityRole>
            {
                new SecurityRole{ Id = 1, Name = "Admin" },
                new SecurityRole{ Id = 1, Name = "Developer" },
                new SecurityRole{ Id = 1, Name = "Reviewer" }
            };

            context.SecurityRoles.Returns(new MockForDbSet<SecurityRole>(roles));

            

            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                context, Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            IHttpActionResult nullUser = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign).Result;

            Assert.IsType<InvalidModelStateResult>(nullUser);
        }

        [Fact]
        public void CheckTheErrorMessageIfRoleNotExists()
        {
            int gitHubId = 1;
            int repoId = 0;
            string roleToAssign = "BussinessAnalytics";
            string expectedError = string.Format("Roles '{0}' does not exists in the system", roleToAssign);
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(store);
            List<User> users = new List<User>{
                new User{ProviderId = 1,},
                new User{ProviderId =2, },
                new User{ProviderId=3,},
            };

            userManager.Users.Returns(new MockForEnumerableQuery<User>(users));
            var context = Substitute.For<ISecurityContext>();
            IEnumerable<SecurityRole> roles = new List<SecurityRole>
            {
                new SecurityRole{ Id = 1, Name = "Admin" },
                new SecurityRole{ Id = 1, Name = "Developer" },
                new SecurityRole{ Id = 1, Name = "Reviewer" }
            };

            context.SecurityRoles.Returns(new MockForDbSet<SecurityRole>(roles));



            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                context, Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            IHttpActionResult nullUser = controller.AssignRolesToUser(repoId, gitHubId, roleToAssign).Result;

            InvalidModelStateResult modelStateResult = Assert.IsType<InvalidModelStateResult>(nullUser);
            Assert.Equal(expectedError,modelStateResult.ModelState["role"].Errors.First().ErrorMessage);
        }


        [Fact]
        public void CheckStatusCodeOk()
        {
            
            string roleToAssign = "Admin";
            UserRepositoryRole repositoryRoleToFind = new UserRepositoryRole { Id = 12 };
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
            int gitHubId = userToUpdate.ProviderId;
            var store = Substitute.For<IUserStore<User>>();
            var userManager = Substitute.For<ApplicationUserManager>(store);
            List<User> users = new List<User>{
                userToUpdate,
                new User{ProviderId =2, },
                new User{ProviderId=3,},
            };
    
            userManager.Users.Returns(new MockForEnumerableQuery<User>(users));
            userManager.UpdateAsync(userToUpdate).Returns(IdentityResult.Success);
            var result = new ClaimsIdentity();
            Claim claim =new Claim(roleToAssign, repositoryRoleToFind.Id.ToString());
            userManager.AddClaim(userToUpdate.Id,claim);
            userManager.CreateIdentityAsync(userToUpdate, DefaultAuthenticationTypes.ApplicationCookie).Returns(Task.FromResult(result));
            userManager.AddClaimAsync(userToUpdate.Id, Arg.Any<Claim>()).Returns(IdentityResult.Success);//Task.FromResult(IdentityResult.Success));
            var context = Substitute.For<ISecurityContext>();
            IEnumerable<SecurityRole> roles = new List<SecurityRole>
            {
                new SecurityRole{ Id = 1, Name = "Admin" },
                new SecurityRole{ Id = 2, Name = "Developer" },
                new SecurityRole{ Id = 3, Name = "Reviewer" }
            };
            context.SecurityRoles.Returns(new MockForDbSet<SecurityRole>(roles));

            AccountController controller = new AccountController(Substitute.For<IGithubService>(),
                context, Substitute.For<IAuthService>(), userManager, store,
                Substitute.For<IRoleStore<IdentityRole, string>>());

            IHttpActionResult response = controller.AssignRolesToUser(repositoryRoleToFind.Id, gitHubId, roleToAssign).Result;

            Assert.IsType <OkResult> (response);
        }

        
    }
}
