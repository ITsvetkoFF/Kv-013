using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Services.Interfaces;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Services.Implementations
{
    public class GitHubService : IGitHubService
    {
        #region fields
        private int countWeeksInMonth = 4;
        private readonly HttpClient _httpClient;
        private GraphModel graph;
        #endregion

        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36" }
        };

        public GitHubService()
        {
            this._httpClient = new HttpClient();
        }

        public async Task<List<int>> GetCommitsForUser(string owner, string repository, string token)   //for year
        {
            var requestUri = string.Format("https://api.github.com/repos/{0}/{1}/stats/participation?access_token={2}", owner, repository, token);
            var response = await _httpClient.SendAsync(CreateMessage(HttpMethod.Get, requestUri));

            CommitsFromRepositoryModel commitsFromRepository = JsonConvert.DeserializeObject<CommitsFromRepositoryModel>(await response.Content.ReadAsStringAsync());

            List<int> ListofCommits = new List<int>();

            //create of the numbers of the month
            for (int i = 0; i < commitsFromRepository.CommitsOwner.Count; i += countWeeksInMonth)
            {
                ListofCommits.Add(commitsFromRepository.CommitsOwner.Skip(i).Take(countWeeksInMonth).Sum(x => x));
            }

            return ListofCommits;
        }

        public async Task<List<RepositoryModel>> GetRepositories(string owner, string token)
        {
            var requestUri = string.Format("https://api.github.com/users/{0}/repos?access_token={1}", owner,token);
            var response = await _httpClient.SendAsync(CreateMessage(HttpMethod.Get, requestUri));
            return JsonConvert.DeserializeObject<List<RepositoryModel>>(await response.Content.ReadAsStringAsync());
        }

        public List<string> GetMountsFromDateTo(DateTime from, DateTime to)
        {
            List<string> months = new List<string>();

            int countMonthInFromTo = to.Month - from.Month;
            if (to.Year != from.Year)
            {
                int mounthInYear = 12;
                countMonthInFromTo += mounthInYear * (to.Year - from.Year);
            }

            for (int i = 0; i < countMonthInFromTo; i++)
            {
                months.Add(CultureInfo.
                    CurrentCulture.
                    DateTimeFormat.
                    GetMonthName(from.AddMonths(i).Month));
            }
            return months;
        }

        public async Task<int> GetFollowerCount(string owner, string token)
        {
            var requestUri = string.Format("https://api.github.com/users/{0}/followers?access_token={1}", owner, token);
            var response = await _httpClient.SendAsync(CreateMessage(HttpMethod.Get, requestUri));
            List<GitHubUserModel> followers =
                JsonConvert.DeserializeObject<List<GitHubUserModel>>(await response.Content.ReadAsStringAsync());   
            return followers.Count;
        }

        public async Task<int> GetFolowingCount(string owner, string token)
        {
            var requestUri = string.Format("https://api.github.com/users/{0}/following?access_token={1}", owner, token);
            var response = await _httpClient.SendAsync(CreateMessage(HttpMethod.Get, requestUri));
            List<GitHubUserModel> listOfFolowing =
                JsonConvert.DeserializeObject<List<GitHubUserModel>>(await response.Content.ReadAsStringAsync());
            return listOfFolowing.Count;
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
