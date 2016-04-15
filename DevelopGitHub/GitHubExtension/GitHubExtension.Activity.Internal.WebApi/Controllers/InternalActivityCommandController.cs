using System;
using System.Security.Claims;
using System.Web.Http;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Commands;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Activity.Internal.WebApi.Controllers
{
    public class InternalActivityCommandController : ApiController
    {
        private IContextActivityCommand _contextActivityCommand;
        private IContextActivityQuery _contextActivityQuery;

        public InternalActivityCommandController(IContextActivityCommand contextActivityCommand, IContextActivityQuery contextActivityQuery)
        {
            _contextActivityCommand = contextActivityCommand;
            _contextActivityQuery = contextActivityQuery;
        }

        [HttpPost]
        [Route(ActivityRouteConstants.AddRoleActivityForCurrentRepository)]
        public IHttpActionResult AddRoleActivityForCurrentRepository([FromBody] string roleToAssign, [FromBody] string collaboratorName)
        {
            Claim currProjectClaim = User.GetCurrentProjectClaim();
            int currentRepositoryId;

            if (currProjectClaim == null || !int.TryParse(currProjectClaim.Value, out currentRepositoryId))
                return BadRequest();

            var activityType = _contextActivityQuery.GetUserActivityType(ActivityTypeNames.AddRole);

            _contextActivityCommand.AddActivity(new ActivityEvent
            {
                UserId = User.Identity.GetUserId(),
                CurrentRepositoryId = currentRepositoryId,
                ActivityType = activityType,
                InvokeTime = DateTime.Now,
                Message =
                    String.Format("{0} {1} {2} to {3}", User.Identity.Name, activityType.Name, roleToAssign, collaboratorName)
            });

            return Ok();
        }

        [HttpPost]
        [Route(ActivityRouteConstants.AddJoinToSystemActivity)]
        public IHttpActionResult AddJoinToSystemActivity([FromBody] string userName, [FromBody] string userId)
        {
            var activityType = _contextActivityQuery.GetUserActivityType(ActivityTypeNames.JoinToSystem);

            _contextActivityCommand.AddActivity(new ActivityEvent()
            {
                UserId = userId,
                ActivityType = activityType,
                InvokeTime = DateTime.Now,
                Message = String.Format("{0} {1}", User.Identity.Name, activityType.Name)
            });

            return Ok();
        }

        [HttpPost]
        [Route(ActivityRouteConstants.RepositoryAddedToSystemActivity)]
        public IHttpActionResult AddRepositoryAddedToSystemActivity([FromBody] string userId, [FromBody] int repositoryId, [FromBody] string repositoryName)
        {
            var repositoryActivityType = _contextActivityQuery.GetUserActivityType(ActivityTypeNames.RepositoryAddedToSystem);

            _contextActivityCommand.AddActivity(new ActivityEvent()
            {
                UserId = userId,
                CurrentRepositoryId = repositoryId,
                ActivityType = repositoryActivityType,
                InvokeTime = DateTime.Now,
                Message = String.Format("{0} {1}", repositoryName, repositoryActivityType.Name)
            });

            return Ok();
        }
    }
}
