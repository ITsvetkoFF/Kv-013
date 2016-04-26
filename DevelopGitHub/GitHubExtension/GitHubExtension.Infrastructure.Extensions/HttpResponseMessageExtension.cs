using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using GitHubExtension.Infrastructure.Constants;

namespace GitHubExtension.Infrastructure.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static bool IsEmptyResponse(this HttpResponseMessage response)
        {
            return response.StatusCode == HttpStatusCode.NotFound;
        }

        public static bool TryGetLinkHeaderFromResponse(
            this HttpResponseMessage response, 
            out IEnumerable<string> requestLinkHeader)
        {
            return response.Headers.TryGetValues(GitHubConstants.LinkHeader, out requestLinkHeader);
        }
    }
}