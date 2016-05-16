using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Http.Results;
using FluentAssertions;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.Tests.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Controllers;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Security.Tests2.Mocks;
using NSubstitute;
using Xunit;

namespace GitHubExtension.Activity.Internal.Tests.TestsForControllers
{
    public class InternalActivityCommandControllerTests
    {
        private const string UserId = "097889d8-cc9e-41b0-8641-6ecee086bf64";

        private const string UserName = "Test";

        private const int CurrentRepositoryId = 15;

        private const string TypeOfClaim = "CurrentProjectId";

        public static IEnumerable<object[]> DataForOkResultForJoinToSystem
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

        public static IEnumerable<object[]> DataForOkResultForAddRole
        {
            get
            {
                yield return new object[] 
                { 
                   new List<ActivityType>()
                   {
                       new ActivityType() { Id = 3, Name = "add role"}
                   },

                   "add role",

                   CurrentRepositoryId.ToString(),

                   new RoleActivityModel() { CollaboratorName = "jhonSkeet", RoleToAssign = "Reviewer"},               
                };
            }
        }

        public static IEnumerable<object[]> DataForBadRequestResultAddRole
        {
            get
            {
                yield return new object[] 
                {
                   new List<ActivityType>()
                   {
                      new ActivityType() { Id = 3, Name = "add role"}
                   },

                   "add role",

                   "null",

                   new RoleActivityModel() { CollaboratorName = "jhonSkeet", RoleToAssign = "Reviewer"},
                                      
                };
            }
        }

        public static IEnumerable<object[]> DataForOkResultForAddRepositoryToSystem
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

        private static InternalActivityCommandController GetControllerInstance(List<ActivityType> activityTypes, string activityTypeName)
        {

            var activityContextQuery = MockForActivityContextQuery(activityTypes);

            var activityType = activityContextQuery.GetUserActivityType(activityTypeName);

            var activityContextCommand = MockForActivityContextCommand();

            ActivityEvent activityEvent = new ActivityEvent()
            {
                UserId = UserId,
                ActivityTypeId = activityType.Id
            };

            activityContextCommand.AddActivity(activityEvent);

            var activityController = new InternalActivityCommandController(activityContextCommand, activityContextQuery);

            return activityController;
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
            };

            activityContextCommand.AddActivity(activityEvent);

            var activityController = new InternalActivityCommandController(activityContextCommand, activityContextQuery);

            return activityController;
        }

        private static InternalActivityCommandController GetControllerInstance(List<ActivityType> activityTypes, string currentProjectId, string activityTypeName)
        {
            var activityContextQuery = MockForActivityContextQuery(activityTypes);

            var activityType = activityContextQuery.GetUserActivityType(activityTypeName);

            var activityContextCommand = MockForActivityContextCommand();

            ActivityEvent activityEvent = new ActivityEvent()
            {
                UserId = UserId,
                CurrentRepositoryId = CurrentRepositoryId,
                ActivityTypeId = activityType.Id
            };

            activityContextCommand.AddActivity(activityEvent);

            var activityController = new InternalActivityCommandController(activityContextCommand, activityContextQuery)
            {
                User = Substitute.For<IPrincipal>().SetUserForController(UserName, TypeOfClaim, currentProjectId)
            };

            return activityController;
        }

        [Theory]
        [MemberData("DataForOkResultForJoinToSystem")]
        public void ShouldReturnOkResultWhenJoinToSystemActivityAdded(List<ActivityType> activityTypes, string activityTypeName)
        {
            // Arrange
            var activityController = GetControllerInstance(activityTypes, activityTypeName);

            // Act
            var response = activityController.AddJoinToSystemActivity();

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Theory]
        [MemberData("DataForOkResultForAddRole")]
        public void ShouldReturnOkResultWhenRoleActivityAdded(List<ActivityType> activityTypes, string activityTypeName, string currentProjectId, RoleActivityModel roleActivityModel)
        {
            // Arrange
            var activityController = GetControllerInstance(activityTypes, currentProjectId, activityTypeName);

            // Act
            var response = activityController.AddRoleActivityForCurrentRepository(roleActivityModel);

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Theory]
        [MemberData("DataForBadRequestResultAddRole")]
        public void ShouldReturnBadRequestResultForRoleActivityAddingWhenWeHaveNoClaimsForCurrentProject(List<ActivityType> activityTypes, string activityTypeName, string currentProjectId, RoleActivityModel roleActivityModel)
        {
            // Arrange
            var activityController = GetControllerInstance(activityTypes, currentProjectId, activityTypeName);

            // Act
            var response = activityController.AddRoleActivityForCurrentRepository(roleActivityModel);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [MemberData("DataForOkResultForAddRepositoryToSystem")]
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

