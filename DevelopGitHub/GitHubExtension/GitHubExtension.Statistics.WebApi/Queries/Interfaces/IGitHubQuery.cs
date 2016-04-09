using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.Queries.Interfaces
{
    public interface IGitHubQuery
    {
        Task<List<int>> GetUserCommitsInYear(string owner, string repository, string token);
        Task<List<RepositoryModel>> GetRepositories(string owner, string token);
        Task<int> GetFollowersCount(string owner, string token);
        Task<int> GetFolowingCount(string owner, string token);
    }
}
