using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Activity.External.WebAPI.Extensions;
using GitHubExtension.Activity.External.WebAPI.Models;
using GitHubExtension.Activity.External.WebAPI.Queries;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Activity.External.WebAPI.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        public ActivityController(IGitHubEventsQuery eventsQuery)
        {
            _eventsQuery = eventsQuery;
        }

        private IGitHubEventsQuery _eventsQuery;

        [Route(ExternalActivityRoutes.GetGitHubActivityRoute)]
        public async Task<IHttpActionResult> GetGitHubActivity([FromUri] int page)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            string fullRepositoryName = claimsIdentity.FindFirstValue("CurrentProjectName");
            Claim tokenClaim = claimsIdentity.FindFirst("ExternalAccessToken");

           
            IEnumerable<GitHubEventModel> events =
                await _eventsQuery.GetGitHubEventsAsync(fullRepositoryName, tokenClaim.Value, page);
            int? numberOfPages = _eventsQuery.GetNumberOfPages();

            EventsPaginationModel model = new EventsPaginationModel() {Events = events, AmountOfPages = numberOfPages};
            return Ok(model);
        }
    }
}
