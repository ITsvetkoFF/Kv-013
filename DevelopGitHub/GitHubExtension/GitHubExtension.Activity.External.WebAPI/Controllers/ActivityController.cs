using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Activity.External.WebAPI.Services;
using GitHubExtension.Security.WebApi.Library.Exceptions;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        public const string GetGitHubActivityRoute = "api/activity/external";
        private IGitHubService _gitHubService;

        public ActivityController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [Route(GetGitHubActivityRoute)]
        public async Task<IHttpActionResult> GetGitHubActivity([FromUri] int page)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            string fullRepositoryName = claimsIdentity.FindFirstValue("CurrentProjectName");
            Claim tokenClaim = claimsIdentity.FindFirst("ExternalAccessToken");

            if (tokenClaim == null)
                throw new TokenNotFoundException();
            
            var events =
                await _gitHubService.GetGitHubEventsAsync(fullRepositoryName, tokenClaim.Value, page);
            int? numberOfPages = _gitHubService.GetNumberOfPages();
            JObject response = new JObject();

            response["events"] = new JArray(events);
            if (numberOfPages.HasValue)
                response["pages"] = numberOfPages;

            return Ok(response);
        }
    }
}
