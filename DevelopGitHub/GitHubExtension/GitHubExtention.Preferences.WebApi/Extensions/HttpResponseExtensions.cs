using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubExtention.Preferences.WebApi.Constants;

namespace GitHubExtention.Preferences.WebApi.Extensions
{
    public static class HttpResponseExtensions
    {
        public static string GetContentType(this Task<HttpResponseMessage> response)
        {
            return response.Result.Content.Headers
                    .FirstOrDefault(x => x.Key == AvatarConstants.ContentType).Value.FirstOrDefault();
        }
    }
}
