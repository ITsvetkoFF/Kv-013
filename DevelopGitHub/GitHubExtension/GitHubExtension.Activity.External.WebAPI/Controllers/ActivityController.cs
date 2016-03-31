using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Activity.External.WebAPI.Services;

namespace GitHubExtension.Activity.External.WebAPI.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        private GitHubService _gitHubService;

        public ActivityController()
        {
            _gitHubService = new GitHubService();
        }

        [Route("api/activities")]
        public async Task<IHttpActionResult> GetGitHubActivity()
        {
            var events = await _gitHubService.GetGitHubEventsAsync("ITsvetkoFF", "Kv-013", "7c021f975ee1e7e3209b9113d9346b7d5bb169f8");
            
            return Ok(events);
        }
    }
}
