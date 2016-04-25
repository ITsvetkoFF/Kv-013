using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Controllers;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.Tests.Mocks;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Activity.Internal.Tests.TestsForControllers
{
    public class ActivityInternalCommandControllerRepositoryAddedToSystem
    {
        private const string UserId = "097889d8-cc9e-41b0-8641-6ecee086bf64";

        public static IEnumerable<object[]> DataForOkResult
        {
            get
            {
                yield return new object[] 
                { 
                   new List<ActivityType>()
                   {
                       new ActivityType() { Id = 2, Name = "repository added to system"}
                   },

                   "repository added to system",

                   new RepositoryActivityModel() { RepositoryId = 1, RepositoryName = "GitHubExtension"}               
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

        private static InternalActivityCommandController GetControllerInstance(List<ActivityType> activityTypes, RepositoryActivityModel repositoryActivityModel, string activityTypeName)
        {
            var activityContextQuery = MockForActivityContextQuery(activityTypes);

            var activityType = activityContextQuery.GetUserActivityType(activityTypeName);

            var activityContextCommand = MockForActivityContextCommand();

            ActivityEvent activityEvent = new ActivityEvent()
            {
                UserId = UserId,
                CurrentRepositoryId = repositoryActivityModel.RepositoryId,
                ActivityTypeId = activityType.Id,
                InvokeTime = DateTime.Now,
                Message = String.Format("{0} {1}", repositoryActivityModel.RepositoryName, activityTypeName)
            };

            activityContextCommand.AddActivity(activityEvent);

            var activityController = new InternalActivityCommandController(activityContextCommand, activityContextQuery);

            return activityController;
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void ShouldReturnOkResultWhenRepositoryAddedToSystemActivityAdded(List<ActivityType> activityTypes, string activityTypeName, RepositoryActivityModel repositoryActivityModel)
        {
            // Arrange
            var activityController = GetControllerInstance(activityTypes, repositoryActivityModel, activityTypeName);

            // Act
            var response = activityController.AddRepositoryAddedToSystemActivity(repositoryActivityModel);

            // Assert
            response.Should().BeOfType<OkResult>();
        }
    }
}
