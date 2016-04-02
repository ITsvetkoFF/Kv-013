using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Statistics.WebApi.Models;
using GitHubExtension.Statistics.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Statistics.WebApi.Controllers
{
    public class StatisticController : ApiController
    {
        private readonly IGitHubService _gitHubService;
        public StatisticController(IGitHubService gitHubService)
        {
            this._gitHubService = gitHubService;
        }

        [Authorize]
        [Route("api/user/commits")]
        public async Task<IHttpActionResult> GetCommitsForUser()
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            List<CommitDto> repositories = await _gitHubService.GetCommitsForUser(userName,"OnlineStrore.mArker",token);
            
            return Ok(repositories);
        }
    }
}
