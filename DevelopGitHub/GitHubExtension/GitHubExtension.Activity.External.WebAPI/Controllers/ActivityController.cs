using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Activity.External.WebAPI.Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        private IGitHubService _gitHubService;

        public ActivityController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [Route("api/activity/external")]
        public async Task<IHttpActionResult> GetGitHubActivity([FromUri] int page)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            string fullRepositoryName = claimsIdentity.FindFirstValue("CurrentProjectName");
            
            var events =
                await _gitHubService.GetGitHubEventsAsync(fullRepositoryName, "2660337e219b525b1c69cab89b0a0d13b93cb3bb", page);
            int? numberOfPages = _gitHubService.GetNumberOfPages();
            JObject response = new JObject();

            response["events"] = new JArray(events);
            if (numberOfPages.HasValue)
                response["pages"] = numberOfPages;

            return Ok(response);
        }
    }
}
