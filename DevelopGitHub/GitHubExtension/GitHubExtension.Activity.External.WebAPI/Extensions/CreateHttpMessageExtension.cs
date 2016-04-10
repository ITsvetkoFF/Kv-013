using System.Collections.Generic;
using System.Net.Http;
using GitHubExtension.Activity.External.WebAPI.Queries;

namespace GitHubExtension.Activity.External.WebAPI.Extensions
{
    public static class CreateHttpMessageExtension
    {
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
             //Need to set user-agent to access GitHub API, Using Chrome 48
            { GitHubConstants.UserAgentHeader, GitHubConstants.UserAgentHeaderValue }
        };

        public static HttpRequestMessage CreateHttpMessage(this IGitHubEventsQuery eventsQuery, HttpMethod method, string requestUri)
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
