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
        private ICollection<RepositoryModel> _repositories;
        #endregion

        public StatisticsQuery(IGitHubQuery gitHubQuery)
        {
            this._gitHubQuery = gitHubQuery;
            this._repositories = new List<RepositoryModel>();
        }
        #region methods

        public async Task<int> GetFollowerCount(string userName, string token)
        {
            int countFollower = await _gitHubQuery.GetFollowersCount(userName, token);
            return countFollower;
        }

        public async Task<int> GetFollowingCount(string userName, string token)
        {
            int countFollowing = await _gitHubQuery.GetFolowingCount(userName, token);
            return countFollowing;
        }

        public async Task<int> GetRepositoriesCount(string userName, string token)
        {
            _repositories = await _gitHubQuery.GetRepositories(userName, token);
            int countRepositories = _repositories.Count;
            return countRepositories;
        }

        public async Task<ICollection<string>> GetActivityMonths()
        {
            int countMounthInYear = 12;
            DateTime timeTo = DateTime.Now;
            DateTime timeFrom = GetTimeFrom();
            ICollection<string> _activityMonths = new List<string>();
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
            ICollection<ICollection<int>> _commitsRepositories = new List<ICollection<int>>();
            HttpCookie cookieCommitsRepositories = HttpContext.Current.Request.Cookies["commitsRepositories"];
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
            ICollection<int> _commitsRepository = await _gitHubQuery.GetCommitsRepository(userName, token, repository);
            return _commitsRepository;
        }

        public async Task<ICollection<int>> GetGroupCommits(ICollection<ICollection<int>> commitsEverRepository)
        {
            ICollection<int> _commitsForYear = _gitHubQuery.ToGroupCommits(commitsEverRepository);
            return _commitsForYear;
        }

        public DateTime GetTimeFrom()
        {
            int countDaysInYear = 364;
            DateTime timeFrom = DateTime.Now.AddDays(-countDaysInYear);
            return timeFrom;
        }
        #endregion
    }
}
