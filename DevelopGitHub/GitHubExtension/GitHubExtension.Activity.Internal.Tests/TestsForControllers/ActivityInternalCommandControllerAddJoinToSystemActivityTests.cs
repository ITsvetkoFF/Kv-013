using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Controllers;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.Tests.Mocks;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Activity.Internal.Tests.TestsForControllers
{
    public class ActivityInternalCommandControllerAddJoinToSystemActivityTests
    {
        private const string UserId = "097889d8-cc9e-41b0-8641-6ecee086bf64";

        private const string UserName = "Test";


        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[] 
                { 
                   new List<ActivityType>()
                   {
                       new ActivityType() { Id = 1, Name = "join to system"}
                   },

                   "join to system"       
                };
            }
        }

        private static IActivityContextQuery MockForActivityContextQuery(IEnumerable<ActivityType> activityTypes)
        {

            IActivityContextQuery activityContextQuery = Substitute.For<IActivityContextQuery>();

            activityContextQuery.ActivitiesTypes.Returns(new MockForDbSet<ActivityType>(activityTypes));

            return activityContextQuery;
        }

        private static IActivityContextCommand MockForActivityContextCommand()
        {

            IActivityContextCommand activityContextCommand = Substitute.For<IActivityContextCommand>();

            return activityContextCommand;
        }

        private static InternalActivityCommandController GetControllerInstance(List<ActivityType> activityTypes, string activityTypeName)
        {

            var activityContextQuery = MockForActivityContextQuery(activityTypes);

            var activityType = activityContextQuery.GetUserActivityType(activityTypeName);

            var activityContextCommand = MockForActivityContextCommand();

            ActivityEvent activityEvent = new ActivityEvent()
            {

                UserId = UserId,
                ActivityTypeId = activityType.Id,
                InvokeTime = DateTime.Now,
                Message = String.Format("{0} {1}", UserName, activityType.Name)
            };

            activityContextCommand.AddActivity(activityEvent);

            var activityController = new InternalActivityCommandController(activityContextCommand, activityContextQuery);

            return activityController;
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void ShouldReturnOkResultWhenJoinToSystemActivityAdded(List<ActivityType> activityTypes, string activityTypeName)
        {
            // Arrange
            var activityController = GetControllerInstance(activityTypes, activityTypeName);

            // Act
            var response = activityController.AddJoinToSystemActivity();

            // Assert
            response.Should().BeOfType<OkResult>();
        }
    }
}
