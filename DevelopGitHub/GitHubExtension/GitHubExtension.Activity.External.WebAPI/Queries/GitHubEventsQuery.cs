using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Activity.External.WebAPI.Extensions;
using GitHubExtension.Activity.External.WebAPI.Models;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Queries
{
    class GitHubEventsQuery : IGitHubEventsQuery
    {
        private const string EventsUrl = "https://api.github.com/repos/{0}/events?access_token={1}&page={2}";
        private const string LinkHeader = "Link";

        private IEnumerable<string> _requestLinkHeader;
        private readonly HttpClient _httpClient;
        public IEnumerable<string> RequestLinkHeader
        {
            get { return _requestLinkHeader; }
        }

        public GitHubEventsQuery()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<GitHubEventModel>> GetGitHubEventsAsync(string fullRepositoryName, string token, int numberOfPage)
        {
            var message = this.CreateHttpMessage(HttpMethod.Get, string.Format(EventsUrl, fullRepositoryName, token, numberOfPage));
            HttpResponseMessage response = await _httpClient.SendAsync(message);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return new List<GitHubEventModel>();
            if (!response.IsSuccessStatusCode)
                throw new GitHubRequestException();

            response.Headers.TryGetValues(LinkHeader, out _requestLinkHeader);

            var json = await response.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<IEnumerable<GitHubEventModel>>(json);

           return events;
        }
    }
}
