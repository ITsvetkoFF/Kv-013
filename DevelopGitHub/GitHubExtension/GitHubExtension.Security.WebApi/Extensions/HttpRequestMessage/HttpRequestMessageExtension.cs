using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
