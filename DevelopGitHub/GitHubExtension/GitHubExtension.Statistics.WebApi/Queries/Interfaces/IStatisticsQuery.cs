using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.Queries.Interfaces
{
    public interface IStatisticsQuery
    {
        Task<GraphModel> GraphCreation(string userName, string token);
        Task<List<int>> GetCommitsRepository(string userName, string token, string repository);
    }
}
