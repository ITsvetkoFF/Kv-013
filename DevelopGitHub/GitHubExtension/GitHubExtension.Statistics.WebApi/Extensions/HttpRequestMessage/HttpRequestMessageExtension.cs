using System.Net.Http;
using GitHubExtension.Statistics.WebApi.Mappers.Constant;

namespace GitHubExtension.Statistics.WebApi.Extensions.HttpRequest
{
    public static class HttpRequestMessageExtension
    {
        public static HttpRequestMessage CreateMessage(this HttpMethod method, string requestUri)
        {
            var message = new HttpRequestMessage(method, requestUri);
            foreach (var header in GitHubConstants.DefaultHeaders)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            return message;
        }
    }
}
