using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Constant;
using GitHubExtension.Statistics.WebApi.Services.Interfaces;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Statistics.WebApi.Controllers
{
    public class StatisticController : ApiController
    {
        private readonly IGitHubService _gitHubService;
        private readonly IStatisticsService _statisticsService;

        public StatisticController(IGitHubService gitHubService, IStatisticsService statisticsService1)
        {
            this._gitHubService = gitHubService;
            this._statisticsService = statisticsService1;
        }

        [Route(StatisticsRouteConstants.GetUserCommits)]
        public async Task<IHttpActionResult> GetCommitsForUser()
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            return Ok(await _statisticsService.GraphCreation(userName, token));
        }

       [Route(StatisticsRouteConstants.GetRepoByName)]
        public async Task<IHttpActionResult> GetRepo([FromUri] string name)
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            return Ok(await _gitHubService.GetCommitsForUser(userName, name, token));
        }
    }
}
