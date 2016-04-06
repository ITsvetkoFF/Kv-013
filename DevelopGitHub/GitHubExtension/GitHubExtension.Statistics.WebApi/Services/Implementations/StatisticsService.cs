using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Models;

namespace GitHubExtension.Statistics.WebApi.Services.Implementations
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IGitHubService _gitHubService;
        private List<Graph> graphs;
        int countDaysInYear = 364;

        public StatisticsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<Graph> GraphCreation(string userName, string token)
        {
            //get repositories
            List<Repository> repositories = await _gitHubService.GetRepositories(userName, token);
            Graph gr = new Graph();
            graphs = new List<Graph>();


            // use strategy
            gr.UserInfo.RepositoryCount = repositories.Count; // get count of repositories
            gr.UserInfo.FollowerCount = await _gitHubService.GetFollowerCount(userName, token); // get count of followers
            gr.UserInfo.FolowingCount = await _gitHubService.GetFolowingCount(userName, token);

            foreach (var repos in repositories)
            {
                graphs.Add(await _gitHubService.GetCommitsForUser(userName, repos.Name, token));
                // Assign a name for each repository,
                gr.RepositoriesName.Add(repos.Name);
            }

            //the summation of all the commits in the past month
            for (int i = 0; i < graphs.Select(a => a.Commits.Count).ToList().FirstOrDefault(); i++)
            {
                gr.Commits.Add(graphs.Sum(a => a.Commits[i]));
            }

            gr.Months = await _gitHubService.GetMountsFromDateTo(DateTime.Now.AddDays(-countDaysInYear), DateTime.Now);

            //list lists all which comprises repositories
            foreach (var item in graphs)
            {
                gr.CommitsForEverRepository.Add(item.Commits);
            }

            return gr;
        }
    }
}
