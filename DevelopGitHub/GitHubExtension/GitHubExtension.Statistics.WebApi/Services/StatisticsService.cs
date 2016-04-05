using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Models;

namespace GitHubExtension.Statistics.WebApi.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IGitHubService _gitHubService;
        private List<Graph> graphs;
        private Graph gr;
        int countDaysInYear = 364;

        public StatisticsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
            graphs = new List<Graph>();
            gr = new Graph();
        }

        public async Task<Graph> GraphCreation(string userName, string token)
        {
            //get repositories
            List<Repository> repositories = await _gitHubService.GetRepositories(userName, token);
            
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
