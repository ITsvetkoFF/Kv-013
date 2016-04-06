using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.GrahpBuilder.Abstract;
using GitHubExtension.Statistics.WebApi.Models;
using GitHubExtension.Statistics.WebApi.Services.Interfaces;

namespace GitHubExtension.Statistics.WebApi.GrahpBuilder.Implementation
{
    public class StatsBuilder : GraphBuilder
    {
        private readonly IGitHubService _gitHubService;
        int countDaysInYear = 364;
        private ICollection<Repository> repositories;

        public StatsBuilder(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public override async Task SetRepositories(string userName, string token)
        {
            this.Graph.Repositories = await _gitHubService.GetRepositories(userName, token);
        }

        public override void SetRepositoryCount()
        {
            this.Graph.UserInfo.RepositoryCount = this.Graph.Repositories.Count;
        }

        public override async Task SetFollowerCount(string userName, string token)
        {
            this.Graph.UserInfo.FollowerCount =  await _gitHubService.GetFollowerCount(userName, token);
        }

        public override async Task SetFollowingCount(string userName, string token)
        {
            this.Graph.UserInfo.FolowingCount = await _gitHubService.GetFolowingCount(userName, token);
        }

        public override async Task SetAllCommitsForUser(string userName, string token)
        {
            foreach (var repos in this.Graph.Repositories)
            {
                ICollection<int> collection = await _gitHubService.GetCommitsForUser(userName, repos.Name, token);
                this.Graph.CommitsForEverRepository.Add(collection);
            }
        }

        public override void SetGroupCommits()//set commits for along one year in all repositories
        {
            for (int i = 0; i < this.Graph.CommitsForEverRepository.ToList().Select(a=>a.ToList().Count).ToList().FirstOrDefault(); i++)
            {
                this.Graph.Commits.Add(Graph.CommitsForEverRepository.Sum(a => a.ToList()[i]));
            }
        }

        public override void SetMonths(DateTime from, DateTime to)
        {
             this.Graph.Months =  _gitHubService.GetMountsFromDateTo(DateTime.Now.AddDays(-countDaysInYear), DateTime.Now);
        }
    }
}
