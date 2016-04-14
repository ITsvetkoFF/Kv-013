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
        private const string UserAgent = "User-Agent";
        private const string UserAgentContent =
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>

        {
            //Need to set user-agent to access GitHub API, Using Chrome 48
            {
                UserAgent,UserAgentContent
            }
        };

        public TemplatesService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetPullRequestTemplatesAsync(string userName, string repositoryName,
            string pathToFile)
        {
            var requestUri = GetRequestUri(userName, repositoryName, pathToFile);
            var message = CreateMessage(HttpMethod.Get, requestUri);
            var response = await _httpClient.GetResponse(message);

            return response;
        }

        public async Task<HttpResponseMessage> GetIssueTemplateAsync(string userName, string repositoryName, string pathToFile)
        {
            var requestUri = GetRequestUri(userName, repositoryName, pathToFile);
            var message = CreateMessage(HttpMethod.Get, requestUri);
            var response = await _httpClient.GetResponse(message);
            
            return response;
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

        private static string GetRequestUri(string userName, string repositoryName, string pathToFile)
        {
            var requestUri =
                string.Format(RouteToRepository + "{0}/{1}" + Contents + "{2}", userName,
                    repositoryName, pathToFile);
            return requestUri;
        }
    }
}