using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Infrastructure.Extensions;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.ExtensionMethods;
using Newtonsoft.Json;

namespace GitHubExtension.Templates.Commands
{
    public class TemplatesCommand : ITemplatesCommand
    {
        private const string Contents = "/contents/";
        private const string RouteToRepository = "https://api.github.com/repos/";
        private const string AccessToken = "?access_token=";
        private readonly HttpClient _httpClient;
        
        public TemplatesCommand()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpStatusCode> CreateTemplateAsync(CreateUpdateTemplateModel model)
        {
            var templateUri =
                GetTemplatesRequestUriWithToken(model.RepositoryName, model.Path, model.Token);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, templateUri).CreateTemplateMessage(model.Message, model.Content);
            var response = await _httpClient.GetResponse(requestMessage);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateTemplateAsync(CreateUpdateTemplateModel model)
        {
            var templateUri =
                GetTemplatesRequestUriWithToken(model.RepositoryName, model.Path, model.Token);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, templateUri).UpdateTemplateMessage(
                model.Message,
                model.Content,
                model.Sha);
            var response = await _httpClient.GetResponse(requestMessage);

            return response.StatusCode;
        }

        public async Task<string> GetShaTemplate(string repositoryName, string pathToFile, string token)
        {
            var templateUri = GetTemplatesRequestUri(repositoryName, pathToFile, token);
            var message = new HttpRequestMessage(HttpMethod.Get, templateUri).AddHeadersForGitHub();
            var response = await _httpClient.GetResponse(message);

            var messageContent = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<GitHubTemplatesModel>(messageContent);
            var sha = model.Sha;

            return sha;
        }

        private static string GetTemplatesRequestUri(string repositoryName, string pathToFile, string token)
        {
            var requestUri =
                string.Format(
                    RouteToRepository + "{0}" + Contents + "{1}" + AccessToken + "{2}",
                    repositoryName, 
                    pathToFile, 
                    token);
            return requestUri;
        }

        private static string GetTemplatesRequestUriWithToken(string repositoryName, string pathToFile, string token)
        {
            var requestUri =
                string.Format(
                    RouteToRepository + "{0}" + Contents + "{1}" + AccessToken + "{2}",
                    repositoryName,
                    pathToFile,
                    token);
            return requestUri;
        }
    }
}
