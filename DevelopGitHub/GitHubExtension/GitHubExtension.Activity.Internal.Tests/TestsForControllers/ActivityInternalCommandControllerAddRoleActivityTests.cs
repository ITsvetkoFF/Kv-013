using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
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
    public class ActivityInternalCommandControllerAddRoleActivityTests
    {
        private const string UserId = "097889d8-cc9e-41b0-8641-6ecee086bf64";

        private const string UserName = "Test";

        private const int CurrentRepositoryId = 15;

        private const string TypeOfClaim = "CurrentProjectId";

        public static IEnumerable<object[]> DataForOkResult
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

        public static IEnumerable<object[]> DataForBadRequestResult
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

        private static IPrincipal SetUserForController(string name, string typeOfClaim, string valueOfClaim)
        {
            var identity = Substitute.ForPartsOf<GenericIdentity>(name);
            identity.AddClaim(new Claim(typeOfClaim, valueOfClaim));
            var principal = Substitute.ForPartsOf<GenericPrincipal>(identity, new[] { "user" });
            return principal;
        }

        private static InternalActivityCommandController GetControllerInstance(List<ActivityType> activityTypes, RoleActivityModel roleActivityModel, string currentProjectId, string activityTypeName)
        {
            var userForController = SetUserForController(UserName, TypeOfClaim, currentProjectId);

            var activityContextQuery = MockForActivityContextQuery(activityTypes);

            var activityType = activityContextQuery.GetUserActivityType(activityTypeName);

            var activityContextCommand = MockForActivityContextCommand();

            ActivityEvent activityEvent = new ActivityEvent()
            {
                UserId = UserId,
                CurrentRepositoryId = CurrentRepositoryId,
                ActivityTypeId = activityType.Id,
                InvokeTime = DateTime.Now,
                Message =
                    String.Format("{0} {1} {2} to {3}", UserId, activityType.Name, roleActivityModel.RoleToAssign,
                        roleActivityModel.CollaboratorName)
            };

            activityContextCommand.AddActivity(activityEvent);

            var activityController = new InternalActivityCommandController(activityContextCommand, activityContextQuery)
            {
                User = userForController
            };

            return activityController;
        }

        [Theory]
        [MemberData("DataForOkResult")]
        public void ShouldReturnOkResultWhenRoleActivityAdded(List<ActivityType> activityTypes, string activityTypeName, string currentProjectId, RoleActivityModel roleActivityModel)
        {
            // Arrange
            var activityController = GetControllerInstance(activityTypes, roleActivityModel, currentProjectId, activityTypeName);

            // Act
            var response = activityController.AddRoleActivityForCurrentRepository(roleActivityModel);

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Theory]
        [MemberData("DataForBadRequestResult")]
        public void ShouldReturnBadRequestResultForRoleActivityAddingWhenWeHaveNoClaimsForCurrentProject(List<ActivityType> activityTypes, string activityTypeName, string currentProjectId, RoleActivityModel roleActivityModel)
        {
            // Arrange
            var activityController = GetControllerInstance(activityTypes, roleActivityModel, currentProjectId, activityTypeName);

            // Act
            var response = activityController.AddRoleActivityForCurrentRepository(roleActivityModel);

            // Assert
            response.Should().BeOfType<BadRequestResult>();
        }
    }
}
