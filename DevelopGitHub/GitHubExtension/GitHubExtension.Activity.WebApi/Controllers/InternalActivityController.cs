using System;
using GitHubExtension.Activity.Internal.DAL;
using GitHubExtension.Activity.Internal.WebApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GitHubExtension.Activity.Internal.WebApi.Models;
using GitHubExtension.Activity.Internal.WebApi.Mappers;

namespace GitHubExtension.Activity.Internal.WebApi.Controllers
{
    [RoutePrefix("api/activity/internal")] 
    public class InternalActivityController : ApiController
    {
        private readonly IActivityReaderService _activityReaderService;

        public InternalActivityController(IActivityReaderService activityReaderService)
        {
            _activityReaderService = activityReaderService;
        }

        [AllowAnonymous]
        [Route("{currentRepositoryId}")] 
        public async Task<IHttpActionResult> GetCurrentRepositoryUserActivities([FromUri]int currentRepositoryId)
        {
            ICollection<ActivityEvent> userActivitiesForCurrentRepo = _activityReaderService.GetCurrentRepositoryUserActivities(currentRepositoryId);

            ICollection<ActivityEventViewModel> activityViewModels = new List<ActivityEventViewModel>();

            foreach (var userActivity in userActivitiesForCurrentRepo)
            {
                activityViewModels.Add(ActivityEventMapper.ToActivityEventViewModel(userActivity));
            }

            if (activityViewModels != null)
            {
                return Ok(activityViewModels);
            }

            return NotFound();
        }

        [AllowAnonymous]
        [Route("{userId:guid}")]  // Must be in constants 
        public async Task<IHttpActionResult> GetUserActivities([FromUri]string userId)
        {
            ICollection<ActivityEvent> userActivities = _activityReaderService.GetUserActivities(userId);

            ICollection<ActivityEventViewModel> activityViewModels = new List<ActivityEventViewModel>();

            foreach (var userActivity in userActivities)
            {
                activityViewModels.Add(ActivityEventMapper.ToActivityEventViewModel(userActivity));
            }

            if (activityViewModels != null)
            {
                return Ok(activityViewModels);
            }

            return NotFound();
        } 
    }
}
