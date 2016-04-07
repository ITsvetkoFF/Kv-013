using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.Services.Interfaces
{
    public interface IGitHubService
    {
        Task<List<int>> GetCommitsForUser(string owner, string repository, string token);
        Task<List<RepositoryModel>> GetRepositories(string owner, string token);
        List<string> GetMountsFromDateTo(DateTime from, DateTime to);
        Task<int> GetFollowerCount(string owner, string token);
        Task<int> GetFolowingCount(string owner, string token);
    }
}
