using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Activity.External.WebAPI.Services;
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
            //This part is moved to extension method in another branch
//            var claims = User.Identity as ClaimsIdentity;
//            string token = claims.FindFirstValue("ExternalAccessToken");
//            string currentProjectName = claims.FindFirstValue("CurrentProjectName");

//            if (token == null)
//                throw new TokenNotFoundException();

//            string userName = User.Identity.GetUserName();

//            var events = await _gitHubService.GetGitHubEventsAsync(userName, currentProjectName, token, page);
            var events =
                await _gitHubService.GetGitHubEventsAsync("ITsvetkoff", "Kv-013", "2660337e219b525b1c69cab89b0a0d13b93cb3bb", page);
            int? numberOfPages = _gitHubService.GetNumberOfPages();
            JObject response = new JObject();

            response["events"] = new JArray(events);
            if (numberOfPages.HasValue)
                response["pages"] = numberOfPages;

            return Ok(response);
        }
    }
}
