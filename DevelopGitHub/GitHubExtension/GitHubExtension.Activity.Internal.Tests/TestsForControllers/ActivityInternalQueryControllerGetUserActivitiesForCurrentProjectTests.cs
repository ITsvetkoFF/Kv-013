using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Controllers;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.Tests.Mocks;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Activity.Internal.Tests.TestsForControllers
{
    public class ActivityInternalQueryControllerGetUserActivitiesForCurrentProjectTests
    {
        private const string UserId = "097889d8-cc9e-41b0-8641-6ecee086bf64";

        private const string UserName = "Test";

        private const int CurrentRepositoryId = 15;

        private const int ActivityTypeId = 3;

        private const string TypeOfClaim = "CurrentProjectId";

        private const string Message = "Test Message";


        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[] 
                { 
                    new List<ActivityEvent> 
                    {
                        new ActivityEvent()
                        {
                            Id = 1,
                            UserId = UserId,
                            ActivityTypeId   = ActivityTypeId,
                            CurrentRepositoryId = CurrentRepositoryId,
                            InvokeTime = DateTime.Now,
                            Message = Message
                        }
                    },

                    CurrentRepositoryId.ToString() 
                };
            }
        }

        public static IEnumerable<object[]> DataForNotFoundResult
        {
            get
            {
                yield return new object[] 
                {
                    new List<ActivityEvent>(),
 
                    CurrentRepositoryId.ToString() 
                };
            }
        }

        public static IEnumerable<object[]> DataForBadRequestResult
        {
            get
            {
                yield return new object[] 
                {
                    new List<ActivityEvent> 
                    {
                        new ActivityEvent()
                        {
                            Id = 1,
                            UserId = UserId,
                            ActivityTypeId   = ActivityTypeId,
                            CurrentRepositoryId = CurrentRepositoryId,
                            InvokeTime = DateTime.Now,
                            Message = Message
                        }
                    },
 
                    "null"
                };
            }
        }

        private static IActivityContextQuery MockForActivityContextQuery(IEnumerable<ActivityEvent> activities)
        {

            IActivityContextQuery activityContextQuery = Substitute.For<IActivityContextQuery>();

            activityContextQuery.Activities.Returns(new MockForDbSet<ActivityEvent>(activities));

            return activityContextQuery;
        }

        private static IPrincipal SetUserForController(string name, string typeOfClaim, string valueOfClaim)
        {
            var identity = Substitute.ForPartsOf<GenericIdentity>(name);
            identity.AddClaim(new Claim(typeOfClaim, valueOfClaim));
            var principal = Substitute.ForPartsOf<GenericPrincipal>(identity, new[] { "user" });
            return principal;
        }

        private static InternalActivityQueryController GetControllerInstance(IEnumerable<ActivityEvent> activities, string currentProjectId)
        {
            var userForController = SetUserForController(UserName, TypeOfClaim, currentProjectId);

            var activityContextQuery = MockForActivityContextQuery(activities);

            activityContextQuery.GetCurrentRepositoryUserActivities(CurrentRepositoryId);

            var activityController = new InternalActivityQueryController(activityContextQuery)
            {
                User = userForController
            };

            return activityController;
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void ShouldReturnOkResultWhenWeGetActivities(IEnumerable<ActivityEvent> activities, string currentProjectId)
        {
            // Arrange
            var activityController = GetControllerInstance(activities, currentProjectId);

            // Act
            var response = activityController.GetCurrentRepositoryUserActivities();

            // Assert
            response.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<ActivityEventModel>>>();
        }

        [Theory]
        [MemberData("DataForNotFoundResult")]
        public void ShouldReturnNotFoundResultWhenActivitiesDoesNotExist(IEnumerable<ActivityEvent> activities, string currentProjectId)
        {
            // Arrange
            var activityController = GetControllerInstance(activities, currentProjectId);

            // Act
            var response = activityController.GetCurrentRepositoryUserActivities();

            // Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [MemberData("DataForBadRequestResult")]
        public void ShouldReturnBadRequestResultWhenWeHaveNoCurrentProjectInClaims(IEnumerable<ActivityEvent> activities, string currentProjectId)
        {
            // Arrange
            var activityController = GetControllerInstance(activities, currentProjectId);

            // Act
            var response = activityController.GetCurrentRepositoryUserActivities();

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }
    }
}
