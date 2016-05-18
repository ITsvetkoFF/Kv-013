using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.Tests.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Controllers;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.Test.Mocks;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Activity.Internal.Tests.TestsForControllers
{
    public class InternalActivityQueryControllerTests
    {
        private const string UserId = "097889d8-cc9e-41b0-8641-6ecee086bf64";

        private const string FakeUserId = "063889d8-cc9e-41b0-8741-6ecee086bt87";

        private const string UserName = "Test";

        private const int CurrentRepositoryId = 15;

        private const int ActivityTypeId = 3;

        private const string TypeOfClaim = "CurrentProjectId";

        private const string Message = "Test Message";

        public static IEnumerable<object[]> DataForOkResultForGetUserActivitiesForRepository
        {
            get
            {
                yield return new object[] 
                { 
                    new List<ActivityEvent> 
                    {
                        new ActivityEvent()
                        {
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

        public static IEnumerable<object[]> DataForNotFoundResultForGetUserActivitiesForRepository
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

        public static IEnumerable<object[]> DataForBadRequestResultForGetUserActivitiesForRepository
        {
            get
            {
                yield return new object[] 
                {
                    new List<ActivityEvent> 
                    {
                        new ActivityEvent()
                        {
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

        public static IEnumerable<object[]> DataForOkResultForGetUserActivitiesTests
        {
            get
            {
                yield return new object[] 
                { 
                    new List<ActivityEvent> 
                    {
                        new ActivityEvent()
                        {
                            UserId = UserId,
                            ActivityTypeId   = ActivityTypeId,
                            CurrentRepositoryId = CurrentRepositoryId,
                            InvokeTime = DateTime.Now,
                            Message = Message
                        }
                    },

                    UserId
                };
            }
        }

        public static IEnumerable<object[]> DataForNotFoundResultForGetUserActivitiesTests
        {
            get
            {
                yield return new object[] 
                {
                    new List<ActivityEvent>(),
 
                    FakeUserId
                };
            }
        }

        private static IActivityContextQuery MockForActivityContextQuery(IEnumerable<ActivityEvent> activities)
        {

            IActivityContextQuery activityContextQuery = Substitute.For<IActivityContextQuery>();

            activityContextQuery.Activities.Returns(new MockForDbSet<ActivityEvent>(activities));

            return activityContextQuery;
        }


        private static InternalActivityQueryController GetControllerInstance(IEnumerable<ActivityEvent> activities)
        {

            var activityContextQuery = MockForActivityContextQuery(activities);

            activityContextQuery.GetUserActivities(UserId);

            var activityController = new InternalActivityQueryController(activityContextQuery);
       

            return activityController;
        }

        private static InternalActivityQueryController GetControllerInstance(IEnumerable<ActivityEvent> activities, string currentProjectId)
        {
           
            var activityContextQuery = MockForActivityContextQuery(activities);

            activityContextQuery.GetCurrentRepositoryUserActivities(CurrentRepositoryId);

            var activityController = new InternalActivityQueryController(activityContextQuery)
            {
                User = Substitute.For<IPrincipal>().SetUserForController(UserName, TypeOfClaim, currentProjectId)
            };

            return activityController;
        }

        [Theory]
        [MemberData("DataForOkResultForGetUserActivitiesForRepository")]
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
        [MemberData("DataForNotFoundResultForGetUserActivitiesForRepository")]
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
        [MemberData("DataForBadRequestResultForGetUserActivitiesForRepository")]
        public void ShouldReturnBadRequestResultWhenWeHaveNoCurrentProjectInClaims(IEnumerable<ActivityEvent> activities, string currentProjectId)
        {
            // Arrange
            var activityController = GetControllerInstance(activities, currentProjectId);

            // Act
            var response = activityController.GetCurrentRepositoryUserActivities();

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [MemberData("DataForOkResultForGetUserActivitiesTests")]
        public void ShouldReturnOkResultWhenWeGetUserActivities(IEnumerable<ActivityEvent> activities, string userId)
        {
            // Arrange
            var activityController = GetControllerInstance(activities);

            // Act
            var response = activityController.GetUserActivities(userId);

            // Assert
            response.Should().BeOfType<OkNegotiatedContentResult<IEnumerable<ActivityEventModel>>>();
        }

        [Theory]
        [MemberData("DataForNotFoundResultForGetUserActivitiesTests")]
        public void ShouldReturnNotFoundResultWhenWeHaveNoActivities(IEnumerable<ActivityEvent> activities, string userId)
        {
            // Arrange
            var activityController = GetControllerInstance(activities);

            // Act
            var response = activityController.GetUserActivities(userId);

            // Assert   
            response.Should().BeOfType<NotFoundResult>();
        }
    }
}
