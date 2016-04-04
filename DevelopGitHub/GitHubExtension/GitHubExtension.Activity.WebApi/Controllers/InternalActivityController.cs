using System;
using GitHubExtension.Activity.Internal.DAL;
using GitHubExtension.Activity.Internal.WebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GitHubExtension.Activity.Internal.WebApi.Controllers
{
    [RoutePrefix("api/activity/internal")] // must be in constants
    public class InternalActivityController : ApiController
    {
        private readonly IActivityReaderService _activityReaderService;

        public InternalActivityController(IActivityReaderService activityReaderService)
        {
            _activityReaderService = activityReaderService;
        }

        [Route("user/{id:guid}/{currentRepositoryId}")]  // Must be in constants 
        public async Task<IHttpActionResult> GetCurrentRepositoryUserActivities(int currentRepositoryId, string userId)
        {
            ICollection<ActivityEvent> userActivitiesForCurrentRepo = _activityReaderService.GetCurrentRepositoryUserActivities(currentRepositoryId, userId);

            return NotFound();
        } 
    }
}
