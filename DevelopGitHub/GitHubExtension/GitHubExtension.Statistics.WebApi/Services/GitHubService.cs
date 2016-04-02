using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.Models;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36" }
        };

        public GitHubService()
        {
            this._httpClient = new HttpClient();
        }
        public Task<GitHubUserModel> GetUserAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommitDto>> GetCommitsForUser(string owner, string repository, string token)
        {
            var requestUri = string.Format("https://api.github.com/repos/{0}/{1}/commits?access_token={2}", owner, repository, token);
            var message = CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            return JsonConvert.DeserializeObject<List<CommitDto>>(await response.Content.ReadAsStringAsync());
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
