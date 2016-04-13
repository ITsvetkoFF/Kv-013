using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Extensions.Cookie;
using GitHubExtension.Statistics.WebApi.Mappers.GitHub;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

namespace GitHubExtension.Statistics.WebApi.Queries.Implementations
{
    public class StatisticsQuery : IStatisticsQuery
    {
        #region fields
        private readonly IGitHubQuery _gitHubQuery;
        private DateTime timeTo = DateTime.Now;
        private DateTime timeFrom;
        private int countDaysInYear = 364;
        private int countMounthInYear = 12;
        private ICollection<string> _activityMonths;
        private ICollection<RepositoryModel> _repositories;
        private ICollection<ICollection<int>> _commitsRepositories;
        private ICollection<int> _commitsForYear;
        private ICollection<int> _commitsRepository;
        private int countFollower;
        private int countFollowing;
        private int countRepositories;
        private HttpCookie cookieCommitsRepositories;
        #endregion

        public StatisticsQuery(IGitHubQuery gitHubQuery)
        {
            this._gitHubQuery = gitHubQuery;
            this._activityMonths = new List<string>();
            this._repositories = new List<RepositoryModel>();
            this._commitsRepositories = new List<ICollection<int>>();
            this._commitsForYear = new List<int>();
            this._commitsRepository = new List<int>();
        }
        #region methods

        public async Task<int> GetFollowerCount(string userName, string token)
        {
            countFollower = await _gitHubQuery.GetFollowersCount(userName, token);
            return countFollower;
        }

        public async Task<int> GetFollowingCount(string userName, string token)
        {
            countFollowing = await _gitHubQuery.GetFolowingCount(userName, token);
            return countFollowing;
        }

        public async Task<int> GetRepositoriesCount(string userName, string token)
        {
            _repositories = await _gitHubQuery.GetRepositories(userName, token);
            countRepositories = _repositories.Count;
            return countRepositories;
        }

        public async Task<ICollection<string>> GetActivityMonths()
        {
            timeFrom = DateTime.Now.AddDays(-countDaysInYear);
            int countMonthInFromTo = timeTo.Month - timeFrom.Month;
            if (timeTo.Year != timeFrom.Year)
            {
                countMonthInFromTo += countMounthInYear * (timeTo.Year - timeFrom.Year);
            }

            for (int i = 0; i < countMonthInFromTo; i++)
            {
                _activityMonths.Add(CultureInfo.
                    CurrentCulture.
                    DateTimeFormat.
                    GetMonthName(timeFrom.AddMonths(i).Month));
            }
            return _activityMonths;
        }

        public async Task<ICollection<RepositoryModel>> GetRepositories(string userName, string token)
        {
            _repositories = await _gitHubQuery.GetRepositories(userName, token);
            return _repositories;
        }

        public async Task<ICollection<ICollection<int>>> GetCommitsRepositories(string userName, string token)
        {
            cookieCommitsRepositories = HttpContext.Current.Request.Cookies["commitsRepositories"];
            if (cookieCommitsRepositories == null)
            {
                _repositories = await _gitHubQuery.GetRepositories(userName, token);
                foreach (var item in _repositories)
                {
                    ICollection<int> commitsInYear = await _gitHubQuery.GetCommitsRepository(userName, token, item.Name);
                    ICollection<int> commitsInMouth = _gitHubQuery.ToMounth(commitsInYear);
                    _commitsRepositories.Add(commitsInMouth);
                }

                HttpContext.Current.SetCommitsRepositories(_commitsRepositories);
            }
            else
            {
                _commitsRepositories = HttpContext.Current.GetCommitsRepositories();
            }

            return _commitsRepositories;
        }

        public async Task<ICollection<int>> GetCommitsRepository(string userName, string token, string repository)
        {
            _commitsRepository = await _gitHubQuery.GetCommitsRepository(userName, token, repository);
            return _commitsRepository;
        }

        public async Task<ICollection<int>> GetGroupCommits(ICollection<ICollection<int>> commitsEverRepository)
        {
            _commitsForYear = _gitHubQuery.ToGroupCommits(commitsEverRepository);
            return _commitsForYear;
        }
        #endregion
    }
}
