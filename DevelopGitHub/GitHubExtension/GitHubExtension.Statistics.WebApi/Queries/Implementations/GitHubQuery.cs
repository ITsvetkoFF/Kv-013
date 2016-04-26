using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using GitHubExtension.Infrastructure.Extensions;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Queries.Constant;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Queries.Implementations
{
    public class GitHubQuery : IGitHubQuery
    {
        private readonly HttpClient _httpClient;

        public GitHubQuery()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ICollection<int>> GetCommitsRepository(string owner, string token, string repository)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetRepositoryCommits, owner, repository, token);
            using (var message = new HttpRequestMessage(HttpMethod.Get, requestUri).AddHeadersForGitHub())
            using (var response = await _httpClient.SendAsync(message))
            {
                RepositoryCommitsModel commitsFromRepository = 
                    JsonConvert.DeserializeObject<RepositoryCommitsModel>(await response.Content.ReadAsStringAsync());
                return commitsFromRepository.CommitsOwner;
            }
        }

        public async Task<int> GetFollowersCount(string owner, string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetFollowers, owner, token);
            using (var message = new HttpRequestMessage(HttpMethod.Get, requestUri).AddHeadersForGitHub())
            using (var response = await _httpClient.SendAsync(message))
            {
                List<GitHubUserModel> followers = 
                    JsonConvert.DeserializeObject<List<GitHubUserModel>>(await response.Content.ReadAsStringAsync());
                return followers.Count;
            }
        }

        public async Task<int> GetFolowingCount(string owner, string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetFollowing, owner, token);
            using (var message = new HttpRequestMessage(HttpMethod.Get, requestUri).AddHeadersForGitHub())
            using (var response = await _httpClient.SendAsync(message))
            {
                List<GitHubUserModel> listOfFolowing = 
                    JsonConvert.DeserializeObject<List<GitHubUserModel>>(await response.Content.ReadAsStringAsync());

                return listOfFolowing.Count;
            }
        }

        public async Task<ICollection<RepositoryModel>> GetRepositories(string owner, string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetRepositories, owner, token);
            using (var message = new HttpRequestMessage(HttpMethod.Get, requestUri).AddHeadersForGitHub())
            using (var response = await _httpClient.SendAsync(message))
            {
                ICollection<RepositoryModel> repositories =
                    JsonConvert.DeserializeObject<List<RepositoryModel>>(await response.Content.ReadAsStringAsync());
                return repositories;
            }
        }
    }
}