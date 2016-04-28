using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

using GitHubExtension.Activity.External.WebAPI.Models;
using GitHubExtension.Activity.External.WebAPI.Queries;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Activity.External.WebAPI.Controllers
{
    public class ActivityController : ApiController
    {
        const string AccessTokenClaimType = "ExternalAccessToken";

        const string CurrentProjectNameClaimType = "CurrentProjectName";

        const string YouHaveNoRepositorySelected = "You have no repository selected";

        private readonly IGitHubEventsQuery _eventsQuery;

        public ActivityController(IGitHubEventsQuery eventsQuery)
        {
            _eventsQuery = eventsQuery;
        }

        [Route(ExternalActivityRoutes.GetGitHubActivityRoute)]
        public async Task<IHttpActionResult> GetGitHubActivity([FromUri] int page)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            string fullRepositoryName = claimsIdentity.FindFirstValue(CurrentProjectNameClaimType);
            if (fullRepositoryName == null)
            {
                return BadRequest(YouHaveNoRepositorySelected);
            }

            Claim tokenClaim = claimsIdentity.FindFirst(AccessTokenClaimType);

            EventsPaginationModel model =
                await _eventsQuery.GetGitHubEventsAsync(fullRepositoryName, tokenClaim.Value, page);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}