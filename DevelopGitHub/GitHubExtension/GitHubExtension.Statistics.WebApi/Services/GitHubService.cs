using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;
using GitHubExtension.Statistics.WebApi.Models;
using Newtonsoft.Json;

namespace GitHubExtension.Statistics.WebApi.Services
{
    public class GitHubService : IGitHubService
    {
        #region fields
        private int countWeeksInMonth = 4;
        private int countDaysInYear = 364;
        private readonly HttpClient _httpClient;
        #endregion

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

        public async Task<Graph> GetCommitsForUser(string owner, string repository, string token)   //for year
        {
            var requestUri = string.Format("https://api.github.com/repos/{0}/{1}/stats/participation?access_token={2}", owner, repository, token);
            var response = await _httpClient.SendAsync(CreateMessage(HttpMethod.Get, requestUri));

            CommitsFromRepository commitsFromRepository = JsonConvert.DeserializeObject<CommitsFromRepository>(await response.Content.ReadAsStringAsync());

            Graph graph = new Graph();
            for (int i = 0; i < commitsFromRepository.Alls.Count; i += countWeeksInMonth)
            {
                graph.Commits.Add(commitsFromRepository.Alls.Skip(i).Take(countWeeksInMonth).Sum(x => x));
            }

            graph.Months = await GetMountsFromDateTo(DateTime.Now.AddDays(-countDaysInYear), DateTime.Now);
            return graph;
        }

        public async Task<List<string>> GetMountsFromDateTo(DateTime from, DateTime to)
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
