using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GitHubExtension.Activity.External.WebAPI.Exceptions;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Services
{
    public class GitHubService : IGitHubService
    {
        private const string LastLinkRelRegex = "last";
        private const string LinklRelPageRegex = @"page=(\d+)";
        private readonly HttpClient _httpClient;
        private IEnumerable<string> _lastRequestLinkHeader;
 
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
             //Need to set user-agent to access GitHub API, Using Chrome 48
            { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36" }
        };

        private const string EventsUrl = "https://api.github.com/repos/{0}/{1}/events?access_token={2}&page={3}";

        public GitHubService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<JToken>> GetGitHubEventsAsync(string owner, string repository, string token, int page)
        {
            var message = CreateMessage(HttpMethod.Get, string.Format(EventsUrl, owner, repository, token, page));
            HttpResponseMessage response = await _httpClient.SendAsync(message);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Unsuccessful github request exception");
            _lastRequestLinkHeader = response.Headers.GetValues("Link");

            JArray events = JArray.Parse(await response.Content.ReadAsStringAsync());

            var parsedEvents = events.Children().Select(e => e.ParseEvent());

            return parsedEvents;
        }

        /// <summary>
        /// Used for pagination, gets number of last page from link header of last request
        /// </summary>
        /// <returns> int number of pages or null if we are on last page</returns>
        public int? GetNumberOfPages()
        {
            if (_lastRequestLinkHeader == null)
                throw new LinkHeaderMissingException();

            string link = _lastRequestLinkHeader.FirstOrDefault();
            if (link == null)
                throw new LinkHeaderMissingException();

            string[][] parts = link.Split(',').Select(l => l.Split(';')).ToArray();
            if (parts.Any(p => p.Length != 2))
                throw new LinkHeaderFormatException();
            string[] lastRel = parts.FirstOrDefault(p => Regex.IsMatch(p[1], LastLinkRelRegex));

            // last rel not found in header this means we are on the last page
            if (lastRel == null)
                return null;

            Match lastPageMatch = Regex.Match(lastRel[0], LinklRelPageRegex);
            if (!lastPageMatch.Success || lastPageMatch.Groups.Count != 2)
                throw new LinkHeaderFormatException();

            int lastPage;
            if ( !int.TryParse(lastPageMatch.Groups[1].Value, out lastPage) )
                throw new LinkHeaderFormatException();

            return lastPage;
        }

        private static HttpRequestMessage CreateMessage(HttpMethod method, string requestUri)
        {
            var message = new HttpRequestMessage(method, requestUri);
            foreach (var header in DefaultHeaders)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            return message;
        }
    }
}
