using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.WebApi.Library.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Security.WebApi.Library.Services
{
    // TODO: Create NLog
    public class GithubService : IGithubService
    {
        private readonly HttpClient _httpClient;
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
             //Need to set user-agent to access GitHub API, Using Chrome 48
            { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36" }
        };

        public GithubService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<GitHubUserModel> GetUserAsync(string token)
        {
            // TODO: Incapsulate all requestUri
            var requestUri = string.Format("https://api.github.com/user?access_token={0}", token);
            var message = CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();
            
            var dto = JsonConvert.DeserializeObject<GitHubUserModel>(await response.Content.ReadAsStringAsync());
            dto.Email = dto.Email ?? await GetPrimaryEmailForUser(token);

            return dto;
        }

        public async Task<List<CollaboratorDto>> GetCollaboratorsForRepo(string owner, string repository, string token)
        {
            var requestUri = string.Format("https://api.github.com/repos/{0}/{1}/collaborators?access_token={2}", owner, repository, token);
            var message = CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            return JsonConvert.DeserializeObject<List<CollaboratorDto>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> GetPrimaryEmailForUser(string token)
        {
            var requestUri = string.Format("https://api.github.com/user/emails?access_token={0}", token);
            var message = CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            var emails = JArray.Parse(await response.Content.ReadAsStringAsync());
            var email = "";

            foreach ( var typedEntry in emails.Children()
                .Select(emailEntry => JsonConvert.DeserializeAnonymousType(emailEntry.ToString(), new {Email = "", Primary = false}))
                .Where(typedEntry => typedEntry.Primary) )
            {
                email = typedEntry.Email;
                break;
            }

            return email;
        }

        public async Task<List<RepositoryModel>> GetReposAsync(string token)
        {
            //Geting repos for user
            var requestUri = string.Format("https://api.github.com/user/repos?access_token={0}", token);
            var message = CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);
            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            return JsonConvert.DeserializeObject<List<RepositoryModel>>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Static factory for message creation.
        /// Default message parameters are set here
        /// </summary>
        /// <param name="method">Method of your request</param>
        /// <param name="requestUri">Uri of your request</param>
        /// <returns>HttpRequestMessage</returns>
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