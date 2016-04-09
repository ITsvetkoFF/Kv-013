using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Constant;
using GitHubExtension.Statistics.WebApi.Mappers;
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
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();
            GraphModel graphModel = await _statisticsService.GraphCreation(userName, token);
            
            return Ok(graphModel);
        }

       [Route(StatisticsRouteConstants.GetRepoByName)]
        public async Task<IHttpActionResult> GetRepo([FromUri] string name)
        {
           string token = User.Identity.GetExternalAccessToken();
           string userName = User.Identity.GetUserName();
           List<int> userCommits = await _gitHubService.GetCommitsForUser(userName, name, token);
            
           return Ok(userCommits);
        }
    }
}
