using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using GitHubExtension.Security.WebApi.Exceptions;
using GitHubExtension.Security.WebApi.Extensions.HttpRequestMessage;
using GitHubExtension.Security.WebApi.Models;
using GitHubExtension.Security.WebApi.Queries.Constant;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Security.WebApi.Queries.Implementations
{
    public class GitHubQuery : IGitHubQuery
    {
        private readonly HttpClient _httpClient;

        public GitHubQuery()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<CollaboratorModel>> GetCollaboratorsForRepo(
            string owner, 
            string repository, 
            string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetCollaboratorsRepo, owner, repository, token);
            var message = HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                throw new UnsuccessfullGitHubRequestException();
            }

            List<CollaboratorModel> collaboratorModels =
                JsonConvert.DeserializeObject<List<CollaboratorModel>>(await response.Content.ReadAsStringAsync());
            return collaboratorModels;
        }

        public async Task<string> GetPrimaryEmailForUser(string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetUserEmail, token);
            var message = HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                throw new UnsuccessfullGitHubRequestException();
            }

            var emails = JArray.Parse(await response.Content.ReadAsStringAsync());
            var email = GetEmailFromResponse(emails);
            return email;
        }

        public async Task<List<RepositoryViewModel>> GetReposAsync(string token)
        {
            // Geting repos for user
            var requestUri = string.Format(GitHubQueryConstant.GetRepos, token);
            var message = HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                throw new UnsuccessfullGitHubRequestException();
            }

            List<RepositoryViewModel> collaboratorModels =
                JsonConvert.DeserializeObject<List<RepositoryViewModel>>(await response.Content.ReadAsStringAsync());
            return collaboratorModels;
        }

        public async Task<GitHubUserModel> GetUserAsync(string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetUser, token);
            var message = HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                throw new UnsuccessfullGitHubRequestException();
            }

            var gitHubUser = JsonConvert.DeserializeObject<GitHubUserModel>(await response.Content.ReadAsStringAsync());
            gitHubUser.Email = gitHubUser.Email ?? await GetPrimaryEmailForUser(token);

            return gitHubUser;
        }

        private static string GetEmailFromResponse(JArray emails)
        {
            var emailEntryDefinition = new { Email = string.Empty, Primary = false };
            var email = string.Empty;

            foreach (var typedEntry in emails.Children().Select(emailEntry =>        
                JsonConvert.DeserializeAnonymousType(emailEntry.ToString(), emailEntryDefinition))
                        .Where(typedEntry => typedEntry.Primary))
        {
                email = typedEntry.Email;
                break;
            }

            return email;
        }
    }
}