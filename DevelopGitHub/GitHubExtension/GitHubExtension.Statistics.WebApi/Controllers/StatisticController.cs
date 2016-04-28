using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

using GitHubExtension.Infrastructure.Extensions.Identity;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Constant;
using GitHubExtension.Statistics.WebApi.Extensions.Cookie;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

using Microsoft.AspNet.Identity;

namespace GitHubExtension.Statistics.WebApi.Controllers
{
    public class StatisticController : ApiController
    {
        private readonly IStatisticsQuery _statisticsQuery;

        public StatisticController(IStatisticsQuery statisticsQuery)
        {
            _statisticsQuery = statisticsQuery;
        }

        public RequestContext GetRequestContext
        {
            get
            {
                var context = new HttpContextWrapper(HttpContext.Current);
                var routeData = HttpContext.Current.Request.RequestContext.RouteData;

                var requestContext = new RequestContext(context, routeData);
                return requestContext;
            }
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetActivityMonths)]
        public ICollection<string> GetActivityMonths()
        {
            ICollection<string> activityMonths = _statisticsQuery.GetActivityMonths();
            return activityMonths;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetCommitsRepositories)]
        public async Task<ICollection<ICollection<int>>> GetCommitsRepositories()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            var commitsRepositories = await GetCommitsRepositories(userName, token);

            return commitsRepositories;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetGroupCommits)]
        public async Task<ICollection<int>> GetGroupCommitsForYear()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            ICollection<ICollection<int>> commitsRepositories = await GetCommitsRepositories(userName, token);

            ICollection<int> commitsRepository = _statisticsQuery.GetGroupCommits(commitsRepositories);
            return commitsRepository;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetRepoByName)]
        public async Task<ICollection<int>> GetRepo([FromUri] string name)
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            ICollection<int> commitsRepository = await _statisticsQuery.GetCommitsRepository(userName, token, name);
            ICollection<int> commitsRepositoryMonths = _statisticsQuery.GetToMonths(commitsRepository);
            return commitsRepositoryMonths;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetRepositories)]
        public async Task<ICollection<RepositoryModel>> GetRepositories()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();
            ICollection<RepositoryModel> repositories = await _statisticsQuery.GetRepositories(userName, token);

            return repositories;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetRepositoriesCount)]
        public async Task<int> GetRepositoriesCount()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            int repositoriesCount = await _statisticsQuery.GetRepositoriesCount(userName, token);
            return repositoriesCount;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetFollowers)]
        public async Task<int> GetUserFollower()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            int follower = await _statisticsQuery.GetFollowerCount(userName, token);
            return follower;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetFollowing)]
        public async Task<int> GetUserFollowing()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            int following = await _statisticsQuery.GetFollowingCount(userName, token);
            return following;
        }

        private async Task<ICollection<ICollection<int>>> GetCommitsRepositories(string userName, string token)
        {
            string paramCookie = "commitsRepositories";
            ICollection<ICollection<int>> commitsRepositories;

            var cookieCommitsRepositories = GetRequestContext.HttpContext.Request.Cookies[paramCookie];

            if (cookieCommitsRepositories == null)
            {
                commitsRepositories = await _statisticsQuery.GetCommitsRepositories(userName, token);
                GetRequestContext.SetCommitsRepositories(commitsRepositories);
            }
            else
            {
                commitsRepositories = GetRequestContext.GetCommitsRepositories();
            }

            return commitsRepositories;
        }
    }
}