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
using FluentAssertions;
using System;



namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerGetUserByIdTests
    {
        public static IEnumerable<object[]> GetDataForNotFountResult
        {
            get
            {
                yield return new object[] 
                { 
                    "1", 
                    null,  
                };
            }
        }

        public static IEnumerable<object[]> GetDataForOkResult
        {
            get
            {
                yield return new object[] 
                { 
                    "5", 
                    new User { ProviderId = 5 },
                };
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
        public void NotFoundUserTest(string findUserById, User fakeFoundUser)
        {
            AccountController controller = GetControllerInstance(findUserById, fakeFoundUser);

            Task<IHttpActionResult> NullUser = controller.GetUser(findUserById);

            IHttpActionResult result = NullUser.Result;
            result.Should().BeOfType<NotFoundResult>("Because user with id ={0} doesn't exists in database", findUserById);
        }


        [Theory]
        [MemberData("GetDataForOkResult")]
        public void OkResultTest(string findUserById, User fakeFoundUser)
        {
            AccountController controller = GetControllerInstance(findUserById, fakeFoundUser);

            Task<IHttpActionResult> response = controller.GetUser(findUserById);

            IHttpActionResult result = response.Result;
            result.Should().BeOfType<OkNegotiatedContentResult<UserReturnModel>>();
        }

    }
}
