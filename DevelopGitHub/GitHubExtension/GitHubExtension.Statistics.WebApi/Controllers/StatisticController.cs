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
        private ICollection<string> _activityMonths;
        private ICollection<RepositoryModel> _repositories;
        private ICollection<ICollection<int>> _commitsRepositories;
        private ICollection<int> _commitsRepository;
        private readonly IStatisticsQuery _statisticsQuery;
        #endregion

        public StatisticController(IStatisticsQuery statisticsQuery)
        {
            this._statisticsQuery = statisticsQuery;
            this._repositories = new List<RepositoryModel>();
            this._commitsRepositories = new List<ICollection<int>>();
        }

        #region methods
        [HttpGet]
        [Route(StatisticsRouteConstants.GetFollowers)]
        public async Task<int> GetUserFollower()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            int _follower = await _statisticsQuery.GetFollowerCount(userName, token);
            return _follower;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetFollowing)]
        public async Task<int> GetUserFollowing()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            int _following = await _statisticsQuery.GetFollowingCount(userName, token);
            return _following;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetRepositoriesCount)]
        public async Task<int> GetRepositoriesCount()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

            int _repositoriesCount = await _statisticsQuery.GetRepositoriesCount(userName, token);
            return _repositoriesCount;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetActivityMonths)]
        public async Task<ICollection<string>> GetActivityMonths()
        {
            _activityMonths = await _statisticsQuery.GetActivityMonths();
            return _activityMonths;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetRepositories)]
        public async Task<ICollection<RepositoryModel>> GetRepositories()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();
            _repositories = await _statisticsQuery.GetRepositories(userName, token);

            return _repositories;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetCommitsRepositories)]
        public async Task<ICollection<ICollection<int>>> GetCommitsRepositories()
        {
            string token = User.Identity.GetExternalAccessToken();
            string userName = User.Identity.GetUserName();

           _commitsRepositories = await _statisticsQuery.GetCommitsRepositories(userName, token);

            return _commitsRepositories;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetGroupCommits)]
        public async Task<ICollection<int>> GetGroupCommitsForYear(string userName, string token)
        {
            _commitsRepositories = await _statisticsQuery.GetCommitsRepositories(userName, token);

            _commitsRepository =
                await _statisticsQuery.GetGroupCommits(_commitsRepositories);
            return _commitsRepository;
        }

        [HttpGet]
        [Route(StatisticsRouteConstants.GetRepoByName)]
        public async Task<ICollection<int>> GetRepo([FromUri] string name)
        {
           string token = User.Identity.GetExternalAccessToken();
           string userName = User.Identity.GetUserName();

           _commitsRepository = await _statisticsQuery.GetCommitsRepository(userName, token, name);

           return _commitsRepository;
        }
        #endregion
    }
}
