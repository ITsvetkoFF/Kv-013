using GitHubExtension.Activity.Internal.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Mappers;
using GitHubExtension.Activity.Internal.WebApi.Queries;
using GitHubExtension.Activity.Internal.WebApi.Services;

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
        public IHttpActionResult GetCurrentRepositoryUserActivities([FromUri]int currentRepositoryId)
        {
            ICollection<ActivityEvent> userActivitiesForCurrentRepo = _contextActivityQuery.GetCurrentRepositoryUserActivities(currentRepositoryId);

            if (userActivitiesForCurrentRepo != null)
            {
                ICollection<ActivityEventModel> activityViewModels = userActivitiesForCurrentRepo.Select(userActivity => userActivity.ToActivityEventModel()).ToList();

                return Ok(activityViewModels);
            }

            return NotFound();
        }

        [HttpGet]
        [Route(ActivityRouteConstants.CurrentUserActivityRoute)]
        public IHttpActionResult GetUserActivities([FromUri]string userId)
        {
            ICollection<ActivityEvent> userActivities = _contextActivityQuery.GetUserActivities(userId);

            if (userActivities != null)
            {
                ICollection<ActivityEventModel> activityViewModels = userActivities.Select(userActivity => userActivity.ToActivityEventModel()).ToList();

                return Ok(activityViewModels);
            }

            return NotFound();
        }
    }
}
