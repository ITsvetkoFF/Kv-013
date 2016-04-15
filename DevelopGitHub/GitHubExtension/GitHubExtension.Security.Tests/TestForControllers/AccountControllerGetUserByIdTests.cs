﻿using System.Collections.Generic;
using NSubstitute;
using Xunit;
using GitHubExtension.Security.DAL.Infrastructure;
using Microsoft.AspNet.Identity;
using GitHubExtension.Security.DAL.Interfaces;
using System.Web.Http;
using System.Web.Http.Results;
using System.Threading.Tasks;
using FluentAssertions;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Controllers;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Services;

namespace GitHubExtension.Security.Tests.TestForControllers
{
    public class AccountControllerGetUserByIdTests
    {
        public static IEnumerable<object[]> DataForNotFountResult
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

        public static IEnumerable<object[]> DataForOkResult
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
            AccountController controller = new AccountController(Substitute.For<IGithubService>(), Substitute.For<ISecurityContext>(), userManager);

            return controller;
        }

        [Theory]
        [MemberData("DataForNotFountResult")]
        public void NotFoundUserTest(string findUserById, User fakeFoundUser)
        {
            //Arrange
            AccountController controller = GetControllerInstance(findUserById, fakeFoundUser);

            //Act
            Task<IHttpActionResult> response = controller.GetUser(findUserById);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<NotFoundResult>("Because user with id ={0} doesn't exists in database", findUserById);
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void OkResultTest(string findUserById, User fakeFoundUser)
        {
            //Arrange
            AccountController controller = GetControllerInstance(findUserById, fakeFoundUser);

            //Act
            Task<IHttpActionResult> response = controller.GetUser(findUserById);

            //Assert
            IHttpActionResult result = response.Result;
            result.Should().BeOfType<OkNegotiatedContentResult<UserReturnModel>>();
        }

    }
}
