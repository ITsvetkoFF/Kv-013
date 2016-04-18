using System.Collections.Generic;
using System.Net.Http;

namespace GitHubExtension.Templates.ExtensionMethods
{
    public static class HttpRequestMessageExtension
    {

        private const string UserAgent = "User-Agent";
        private const string UserAgentContent =
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
        private static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>

        {
            //Need to set user-agent to access GitHub API, Using Chrome 48
            {
                UserAgent,UserAgentContent
            }
        };

        public static HttpRequestMessage CreateMessage(this HttpRequestMessage message)
        {
            foreach (var header in DefaultHeaders)
            {
                message.Headers.Add(header.Key, header.Value);
            }
            return message;
        }
    }
}
