using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.GrahpBuilder;
using GitHubExtension.Statistics.WebApi.GrahpBuilder.Abstract;
using GitHubExtension.Statistics.WebApi.GrahpBuilder.Implementation;
using GitHubExtension.Statistics.WebApi.Services.Interfaces;

namespace GitHubExtension.Statistics.WebApi.Services.Implementations
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IGitHubService _gitHubService;
        private ICollection<Graph> graphs;
        int countDaysInYear = 364;

        public StatisticsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<Graph> GraphCreation(string userName, string token)
        {
            Builder builder = new Builder();
            GraphBuilder graphBuilder = new StatsBuilder(_gitHubService);

            await builder.Build(graphBuilder, userName, token, DateTime.Now.AddDays(-countDaysInYear), DateTime.Now);

            return graphBuilder.Graph;
        }
    }
}
