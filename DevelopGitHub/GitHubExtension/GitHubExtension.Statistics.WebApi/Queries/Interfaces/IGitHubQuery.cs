using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.Queries.Interfaces
{
    public interface IGitHubQuery
    {
        Task<ICollection<int>> GetCommitsRepository(string owner, string repository, string token);
        Task<ICollection<RepositoryModel>> GetRepositories(string owner, string token);
        Task<int> GetFollowersCount(string owner, string token);
        Task<int> GetFolowingCount(string owner, string token);
    }
}
