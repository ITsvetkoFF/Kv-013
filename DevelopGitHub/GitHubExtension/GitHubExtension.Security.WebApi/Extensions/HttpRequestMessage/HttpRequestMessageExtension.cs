using System.Net.Http;

using GitHubExtension.Security.WebApi.Constants;

namespace GitHubExtension.Security.WebApi.Extensions.HttpRequestMessage
{
    public static class HttpRequestMessageExtension
    {
        public static System.Net.Http.HttpRequestMessage CreateMessage(this HttpMethod method, string requestUri)
        {
            var message = new System.Net.Http.HttpRequestMessage(method, requestUri);
            foreach (var header in GitHubConstants.DefaultHeaders)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            return message;
        }
    }
}