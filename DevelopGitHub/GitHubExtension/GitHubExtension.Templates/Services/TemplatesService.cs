using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Templates.ExtensionMethods;

namespace GitHubExtension.Templates.Services
{
    public class TemplatesService : ITemplateService
    {
        private readonly HttpClient _httpClient;
        private const string RouteToRepository = "https://api.github.com/repos/";
        private const string Contents = "/contents/";

        public TemplatesService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetPullRequestTemplatesAsync(string userName, string repositoryName,
            string pathToFile)
        {
            var pullRequestTemplateUri = GetTemplatesRequestUri(userName, repositoryName, pathToFile);
            var message = new HttpRequestMessage(HttpMethod.Get, pullRequestTemplateUri).CreateMessage();
            var response = await _httpClient.GetResponse(message);

            return response;
        }

        public async Task<HttpResponseMessage> GetIssueTemplateAsync(string userName, string repositoryName, string pathToFile)
        {
            var issueTemplateUri = GetTemplatesRequestUri(userName, repositoryName, pathToFile);
            var message = new HttpRequestMessage(HttpMethod.Get, issueTemplateUri).CreateMessage();
            var response = await _httpClient.GetResponse(message);
            
            return response;
        }

        private static string GetTemplatesRequestUri(string userName, string repositoryName, string pathToFile)
        {
            var requestUri =
                string.Format(RouteToRepository + "{0}/{1}" + Contents + "{2}", userName,
                    repositoryName, pathToFile);
            return requestUri;
        }
    }
}