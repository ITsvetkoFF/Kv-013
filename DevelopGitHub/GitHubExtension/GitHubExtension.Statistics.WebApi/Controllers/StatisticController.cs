using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Models;
using GitHubExtension.Statistics.WebApi.Services;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Statistics.WebApi.Controllers
{
    public class StatisticController : ApiController
    {
        private readonly IGitHubService _gitHubService;
        private readonly IStatisticsService _statisticsService;
        private Graph gr;

        public StatisticController(IGitHubService gitHubService, IStatisticsService statisticsService1)
        {
            this._gitHubService = gitHubService;
            this._statisticsService = statisticsService1;
            gr = new Graph();
        }

        [Route("api/user/commits")]
        public async Task<IHttpActionResult> GetCommitsForUser()
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            gr = await _statisticsService.GraphCreation(userName, token);

            return Ok(gr);
        }

       [Route("api/user/commits/{name}")]
        public async Task<IHttpActionResult> GetRepo([FromUri] string name)
        {
            var claims = User.Identity as ClaimsIdentity;
            string token = claims.FindFirstValue("ExternalAccessToken");
            string userName = User.Identity.GetUserName();

            Graph graph = await _gitHubService.GetCommitsForUser(userName, name, token);
            
            return Ok(graph);
        }
    }
}
