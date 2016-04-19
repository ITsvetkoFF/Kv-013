using System.Threading.Tasks;
using GitHubExtension.Activity.External.WebAPI.Models;

namespace GitHubExtension.Activity.External.WebAPI.Queries
{
    public interface IGitHubEventsQuery
    {
        Task<EventsPaginationModel> GetGitHubEventsAsync(string fullRepositoryName, string token, int numberOfPage);
    }
}
