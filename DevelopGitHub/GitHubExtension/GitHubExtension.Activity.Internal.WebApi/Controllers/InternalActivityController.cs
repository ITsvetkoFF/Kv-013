using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using GitHubExtension.Activity.DAL;
using GitHubExtension.Activity.Internal.WebApi.Extensions;
using GitHubExtension.Activity.Internal.WebApi.Mappers;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Queries;

namespace GitHubExtension.Activity.Internal.WebApi.Controllers
{
    public class InternalActivityController : ApiController
    {
        private readonly IContextActivityQuery _contextActivityQuery;

        public InternalActivityController(IContextActivityQuery contextActivityQuery)
        {
            _contextActivityQuery = contextActivityQuery;
        }

        [HttpGet]
        [Route(ActivityRouteConstants.CurrentRepositoryActivityRoute)]
        public IHttpActionResult GetCurrentRepositoryUserActivities()
        {
            var identity = User.Identity as ClaimsIdentity;
            Claim currProjectClaim = identity.FindFirst("CurrentProjectId");
            int currentRepositoryId;

            if (currProjectClaim == null || !int.TryParse(currProjectClaim.Value, out currentRepositoryId))
                return BadRequest();

            ICollection<ActivityEvent> userActivitiesForCurrentRepo = _contextActivityQuery.GetCurrentRepositoryUserActivities(currentRepositoryId);

            if (userActivitiesForCurrentRepo.Count == 0)
                return NotFound();
            ICollection<ActivityEventModel> activityViewModels = userActivitiesForCurrentRepo.Select(userActivity => userActivity.ToActivityEventModel()).ToList();

            return Ok(activityViewModels);
        }

        [HttpGet]
        [Route(ActivityRouteConstants.CurrentUserActivityRoute)]
        public IHttpActionResult GetUserActivities([FromUri]string userId)
        {
            ICollection<ActivityEvent> userActivities = _contextActivityQuery.GetUserActivities(userId);

            if (userActivities.Count == 0)
                return NotFound();
            ICollection<ActivityEventModel> activityViewModels = userActivities.Select(userActivity => userActivity.ToActivityEventModel()).ToList();

            return Ok(activityViewModels);
        }
    }
}

