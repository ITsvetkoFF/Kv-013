using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubExtension.Activity.External.WebAPI.Models;

namespace GitHubExtension.Activity.External.WebAPI.Queries
{
    interface IGitHubEventsQuery
    {
        Task<IEnumerable<GitHubEventModel>> GetGitHubEventsAsync(string fullRepositoryName, string token, int numberOfPage);
        IEnumerable<string> RequestLinkHeader { get; }
    }
}
