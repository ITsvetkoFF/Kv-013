using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.BLL.Interfaces
{
    public interface IGraphBll
    {
        Task<ICollection<ICollection<int>>> GetAllCommitsUser(string userName, string token,
            ICollection<RepositoryModel> repositories);
        ICollection<string> GetMountsFromDateTo(DateTime from, DateTime to);
        Task<int> GetFollowingCount(string userName, string token);
        Task<int> GetFollowerCount(string userName, string token);
        Task<List<RepositoryModel>> GetRepositories(string userName, string token);
        ICollection<int> GetGroupCommits(ICollection<ICollection<int>> commitsEverRepository);
    }
}
