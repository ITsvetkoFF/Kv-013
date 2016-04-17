using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Extensions.HttpRequest;
using GitHubExtension.Statistics.WebApi.Queries.Constant;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Queries.Implementations
{
    public class GitHubQuery : IGitHubQuery
    {
        #region fields
        private readonly HttpClient _httpClient;
        #endregion

        public GitHubQuery()
        {
            this._httpClient = new HttpClient();
        }

        #region methods
        public async Task<ICollection<int>> GetCommitsRepository(string owner, string token, string repository)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetRepositoryCommits, owner, repository, token);
            var response = await _httpClient.SendAsync(HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri));
            RepositoryCommitsModel commitsFromRepository = JsonConvert.DeserializeObject<RepositoryCommitsModel>(await response.Content.ReadAsStringAsync());
            return commitsFromRepository.CommitsOwner;
        }

        public async Task<ICollection<RepositoryModel>> GetRepositories(string owner, string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetRepositories, owner, token);
            var response = await _httpClient.SendAsync(HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri));
            ICollection<RepositoryModel> repositories =
                JsonConvert.DeserializeObject<List<RepositoryModel>>(await response.Content.ReadAsStringAsync());
            return repositories;
        }

        public async Task<int> GetFollowersCount(string owner, string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetFollowers, owner, token);
            var response = await _httpClient.SendAsync(HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri));
            List<GitHubUserModel> followers =
                JsonConvert.DeserializeObject<List<GitHubUserModel>>(await response.Content.ReadAsStringAsync());
            return followers.Count;
        }

        public async Task<int> GetFolowingCount(string owner, string token)
        {
            var requestUri = string.Format(GitHubQueryConstant.GetFollowing, owner, token);
            var response = await _httpClient.SendAsync(HttpRequestMessageExtension.CreateMessage(HttpMethod.Get, requestUri));
            List<GitHubUserModel> listOfFolowing =
                JsonConvert.DeserializeObject<List<GitHubUserModel>>(await response.Content.ReadAsStringAsync());
            return listOfFolowing.Count;
        }
        #endregion
    }
}
