using System;
using System.Security.Claims;
using System.Web.Http;

using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Queries;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Activity.Internal.WebApi.Controllers
{
    public class InternalActivityCommandController : ApiController
    {
        private IActivityContextCommand _activityContextCommand;

        private IActivityContextQuery _activityContextQuery;

        public InternalActivityCommandController(
            IActivityContextCommand activityContextCommand, 
            IActivityContextQuery activityContextQuery)
        {
            _activityContextCommand = activityContextCommand;
            _activityContextQuery = activityContextQuery;
        }

        [HttpPost]
        [Route(ActivityRouteConstants.AddJoinToSystemActivity)]
        public IHttpActionResult AddJoinToSystemActivity()
        {
            var activityType = _activityContextQuery.GetUserActivityType(ActivityTypeNames.JoinToSystem);

            ActivityEvent activityEvent = new ActivityEvent()
                                              {
                                                  UserId = User.Identity.GetUserId(), 
                                                  ActivityType = activityType, 
                                                  InvokeTime = DateTime.Now, 
                                                  Message =
                                                      string.Format(
                                                          "{0} {1}", 
                                                          User.Identity.Name, 
                                                          activityType.Name)
                                              };

            _activityContextCommand.AddActivity(activityEvent);

            return Ok();
        }

        [HttpPost]
        [Route(ActivityRouteConstants.RepositoryAddedToSystemActivity)]
        public IHttpActionResult AddRepositoryAddedToSystemActivity(
            [FromBody] RepositoryActivityModel repositoryActivityModel)
        {
            var repositoryActivityType =
                _activityContextQuery.GetUserActivityType(ActivityTypeNames.RepositoryAddedToSystem);

            ActivityEvent activityEvent = new ActivityEvent()
                                              {
                                                  UserId = User.Identity.GetUserId(), 
                                                  CurrentRepositoryId =
                                                      repositoryActivityModel.RepositoryId, 
                                                  ActivityType = repositoryActivityType, 
                                                  InvokeTime = DateTime.Now, 
                                                  Message =
                                                      string.Format(
                                                          "{0} {1}", 
                                                          repositoryActivityModel.RepositoryName, 
                                                          repositoryActivityType.Name)
                                              };

            _activityContextCommand.AddActivity(activityEvent);

            return Ok();
        }

        [HttpPost]
        [Route(ActivityRouteConstants.AddRoleActivityForCurrentRepository)]
        public IHttpActionResult AddRoleActivityForCurrentRepository([FromBody] RoleActivityModel roleActivityModel)
        {
            Claim currProjectClaim = User.GetCurrentProjectClaim();
            int currentRepositoryId;

            if (currProjectClaim == null || !int.TryParse(currProjectClaim.Value, out currentRepositoryId))
            {
                return BadRequest();
            }

            var activityType = _activityContextQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            ActivityEvent activityEvent = new ActivityEvent()
                                              {
                                                  UserId = User.Identity.GetUserId(), 
                                                  CurrentRepositoryId = currentRepositoryId, 
                                                  ActivityType = activityType, 
                                                  InvokeTime = DateTime.Now, 
                                                  Message =
                                                      string.Format(
                                                          "{0} {1} {2} to {3}", 
                                                          User.Identity.Name, 
                                                          activityType.Name, 
                                                          roleActivityModel.RoleToAssign, 
                                                          roleActivityModel.CollaboratorName)
                                              };

            _activityContextCommand.AddActivity(activityEvent);

            return Ok();
        }
    }
}