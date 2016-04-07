using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<GraphModel> GraphCreation(string userName, string token);
    }
}
