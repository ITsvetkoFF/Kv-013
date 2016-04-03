using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.WebApi.Library.Exceptions;
using Newtonsoft.Json;

namespace GitHubExtension.Security.WebApi.Library.Services
{
    internal class TemplatesService : ITemplateService
    {
        private readonly HttpClient _httpClient;

        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>
        {
            //Need to set user-agent to access GitHub API, Using Chrome 48
            {
                "User-Agent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36"
            }
        };

        public TemplatesService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetPullRequestTemplatesAsync(string userName, string repositoryName, 
            string pathToFile)
        {
            var requestUri = String.Format("https://api.github.com/repos/{0}/{1}/{2}",userName,repositoryName,pathToFile);
            var message = CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            var dto = JsonConvert.DeserializeObject<GitHubTemplatesModel>(await response.Content.ReadAsStringAsync());

            var data = Convert.FromBase64String(dto.Content);

            return Encoding.UTF8.GetString(data);
        }

        public async Task<string> GetIssueTemplateAsync(string userName, string repositoryName, string pathToFile)
        {
            var requestUri = String.Format("https://api.github.com/repos/{0}/{1}/{2}", userName, repositoryName, pathToFile);
            var message = CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            var dto = JsonConvert.DeserializeObject<GitHubTemplatesModel>(await response.Content.ReadAsStringAsync());
            var data = Convert.FromBase64String(dto.Content);

            return Encoding.UTF8.GetString(data);
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
