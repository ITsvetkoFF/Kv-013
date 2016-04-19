using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Activity.External.WebAPI.Exceptions;
using GitHubExtension.Activity.External.WebAPI.Extensions;
using GitHubExtension.Activity.External.WebAPI.Models;
using GitHubExtension.Infrastructure.Extensions;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Queries
{
    class GitHubEventsQuery : IGitHubEventsQuery
    {
        private const string EventsUrl = "https://api.github.com/repos/{0}/events?access_token={1}&page={2}";
        private readonly HttpClient _httpClient;

        public GitHubEventsQuery()
        {
            _httpClient = new HttpClient();
        }

        public async Task<EventsPaginationModel> GetGitHubEventsAsync(string fullRepositoryName, string token, int numberOfPage)
        {
            string requestUri = string.Format(EventsUrl, fullRepositoryName, token, numberOfPage);
            using (var message = new HttpRequestMessage(HttpMethod.Get, requestUri).AddHeadersForGitHub())
            using (HttpResponseMessage response = await _httpClient.SendAsync(message))
            {
                if (response.IsEmptyResponse())
                    return null;
                if (!response.IsSuccessStatusCode)
                    throw new GitHubRequestException();

                EventsPaginationModel eventModel = await PreparePaginationModel(response);

                return eventModel;
            }
        }

        private async Task<EventsPaginationModel> PreparePaginationModel(HttpResponseMessage response)
        {
            IEnumerable<string> requestLinkHeader;
            int? numberOfPages = null;

            if (response.TryGetLinkHeaderFromResponse(out requestLinkHeader))
                numberOfPages = this.GetNumberOfPages(requestLinkHeader);

            var json = await response.Content.ReadAsStringAsync();
            var events = DeserialiseEvents(json);
            if (!events.Any())
                return null;

            return new EventsPaginationModel() { AmountOfPages = numberOfPages, Events = events };
        }

        private List<GitHubEventModel> DeserialiseEvents(string json)
        {
            return JsonConvert.DeserializeObject<List<GitHubEventModel>>(json);
        }
    }
}
