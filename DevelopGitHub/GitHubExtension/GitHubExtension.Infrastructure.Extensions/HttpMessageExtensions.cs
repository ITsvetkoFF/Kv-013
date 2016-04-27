using System.Collections.Generic;
using System.Net.Http;

using GitHubExtension.Infrastructure.Constants;

namespace GitHubExtension.Infrastructure.Extensions
{
    public static class HttpMessageExtensions
    {
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
            {
                // Need to set user-agent to access GitHub API
                GitHubConstants.UserAgentHeader, 
                GitHubConstants.UserAgentHeaderValue
            }
        };

        public static HttpRequestMessage AddHeadersForGitHub(this HttpRequestMessage message)
        {
            foreach (var header in DefaultHeaders)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            return message;
        }
    }
}