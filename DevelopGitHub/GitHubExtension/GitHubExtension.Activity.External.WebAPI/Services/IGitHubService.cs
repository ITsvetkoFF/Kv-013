using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Services
{
    public interface IGitHubService
    {
        Task<IEnumerable<JToken>> GetGitHubEventsAsync(string fullRepositoryName, string token, int page);

        /// <summary>
        /// Used for pagination, gets number of last page from link header of last request
        /// </summary>
        /// <returns> int number of pages or null if we are on last page</returns>
        int? GetNumberOfPages();
    }
}