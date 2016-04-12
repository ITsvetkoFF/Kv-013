using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Constant;
using GitHubExtension.Statistics.WebApi.Extensions.Identity;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Statistics.WebApi.Controllers
{
    public class StatisticController : ApiController
    {
        #region fields
        private readonly IStatisticsQuery _statisticsQuery;
        #endregion

        public StatisticController(IStatisticsQuery statisticsQuery)
        {
            this._statisticsQuery = statisticsQuery;
        }

        #region methods
        [HttpGet]
        [Route(StatisticsRouteConstants.GetUserCommits)]
        public async Task<IHttpActionResult> GetCommitsForUser()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();
            GraphModel graph = await _statisticsQuery.GraphCreation(userName, token);

            return Ok(graph);
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetRepoByName)]
        public async Task<IHttpActionResult> GetRepo([FromUri] string name)
        {
           string token = User.Identity.GetExternalAccessToken();
           string userName = User.Identity.GetUserName();

           List<int> userCommits = await _statisticsQuery.GetCommitsRepository(userName, token, name);

           return Ok(userCommits);
        }
        #endregion
    }
}
