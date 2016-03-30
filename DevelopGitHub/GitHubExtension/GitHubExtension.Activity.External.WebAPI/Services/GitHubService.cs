using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
             //Need to set user-agent to access GitHub API, Using Chrome 48
            { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36" }
        };

        public GitHubService()
        {
            _httpClient = new HttpClient();
        }

        public async void GetGitHubEvents(string owner, string repository, string token)
        {
            var message = CreateMessage(HttpMethod.Get, "");
            HttpResponseMessage response = await _httpClient.SendAsync(message);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Unsuccessful github request exception");

            dynamic events = JArray.Parse(await response.Content.ReadAsStringAsync());
            foreach (var @event in events)
                Console.WriteLine(@event);

            //JsonReader json = new JsonTextReader(new StreamReader(await response.Content.ReadAsStreamAsync()));

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
