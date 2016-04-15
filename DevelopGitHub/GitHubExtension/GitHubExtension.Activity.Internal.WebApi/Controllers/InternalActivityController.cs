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
            Claim currProjectClaim = User.GetCurrentProjectClaim();
            int currentRepositoryId;

            if (currProjectClaim == null || !int.TryParse(currProjectClaim.Value, out currentRepositoryId))
                return BadRequest();

            IEnumerable<ActivityEvent> userActivitiesForCurrentRepo = _contextActivityQuery.GetCurrentRepositoryUserActivities(currentRepositoryId);

            if (userActivitiesForCurrentRepo == null)
                return NotFound();
            IEnumerable<ActivityEventModel> activityViewModels = userActivitiesForCurrentRepo.Select(userActivity => userActivity.ToActivityEventModel()).ToList();

            return Ok(activityViewModels);
        }

        [HttpGet]
        [Route(ActivityRouteConstants.CurrentUserActivityRoute)]
        public IHttpActionResult GetUserActivities([FromUri]string userId)
        {
            IEnumerable<ActivityEvent> userActivities = _contextActivityQuery.GetUserActivities(userId);

            if (userActivities == null)
                return NotFound();
            IEnumerable<ActivityEventModel> activityViewModels = userActivities.Select(userActivity => userActivity.ToActivityEventModel());

            return Ok(activityViewModels);
        }
    }
}

