using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.BLL.Interfaces;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Mappers.GitHub;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

namespace GitHubExtension.Statistics.WebApi.BLL.Implementations
{
    public class GraphsBll : IGraphBll
    {
        #region fields
        private int countMounthInYear = 12;
        private readonly IGitHubQuery _gitHubQuery;
        private ICollection<ICollection<int>> commitsForEverRepository;
        private ICollection<int> commitsInYear;
        private ICollection<int> commitsInMonth;
        private ICollection<string> months;
        private ICollection<int> groupCommits;
        #endregion

        public GraphsBll(IGitHubQuery gitHubQuery)
        {
            this._gitHubQuery = gitHubQuery;
            this.commitsForEverRepository = new List<ICollection<int>>();
            this.commitsInYear = new List<int>();
            this.commitsInMonth = new List<int>();
            this.months = new List<string>();
            this.groupCommits = new List<int>();
        }

        #region methods
        public async Task<ICollection<ICollection<int>>> GetAllCommitsUser(string userName, string token, ICollection<RepositoryModel> repositories)
        {
            foreach (var repos in repositories)
            {
                commitsInYear = await _gitHubQuery.GetUserCommitsInYear(userName, repos.Name, token);
                commitsInMonth = _gitHubQuery.ToMounth(commitsInYear);

                commitsForEverRepository.Add(commitsInMonth);
            }

            return commitsForEverRepository;
        }

        public ICollection<string> GetMountsFromDateTo(DateTime from, DateTime to)
        {
            int countMonthInFromTo = to.Month - from.Month;//count difference between to and from
            if (to.Year != from.Year)
            {
                countMonthInFromTo += countMounthInYear * (to.Year - from.Year);
            }

            for (int i = 0; i < countMonthInFromTo; i++)
            {
                months.Add(CultureInfo.
                    CurrentCulture.
                    DateTimeFormat.
                    GetMonthName(from.AddMonths(i).Month));
            }
            return months;
        }

        public Task<int> GetFollowingCount(string userName, string token)
        {
            return _gitHubQuery.GetFolowingCount(userName, token);
        }

        public Task<int> GetFollowerCount(string userName, string token)
        {
            return _gitHubQuery.GetFollowersCount(userName, token);
        }

        public Task<List<RepositoryModel>> GetRepositories(string userName, string token)
        {
            return _gitHubQuery.GetRepositories(userName, token);
        }

        public ICollection<int> GetGroupCommits(ICollection<ICollection<int>> commitsEverRepository)
        {
            groupCommits = _gitHubQuery.ToGroupCommits(commitsEverRepository);

            return groupCommits;
        }
        #endregion
    }
}
