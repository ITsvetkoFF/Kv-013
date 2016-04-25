using System.Collections.Generic;
using System.Threading.Tasks;

using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.Queries.Interfaces
{
    public interface IStatisticsQuery
    {
        ICollection<string> GetActivityMonths();

        Task<ICollection<ICollection<int>>> GetCommitsRepositories(string userName, string token);

        Task<ICollection<int>> GetCommitsRepository(string userName, string token, string repository);

        Task<int> GetFollowerCount(string userName, string token);

        Task<int> GetFollowingCount(string userName, string token);

        ICollection<int> GetGroupCommits(ICollection<ICollection<int>> commitsEverRepository);

        Task<ICollection<RepositoryModel>> GetRepositories(string userName, string token);

        Task<int> GetRepositoriesCount(string userName, string token);

        ICollection<int> GetToMonths(ICollection<int> commits);
    }
}