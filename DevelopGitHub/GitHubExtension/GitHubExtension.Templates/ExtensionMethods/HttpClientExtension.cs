using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubExtension.Templates.ExtensionMethods
{
    public static class HttpClientExtension
    {
        public static async Task<HttpResponseMessage> GetResponse(
            this HttpClient httpClient, 
            HttpRequestMessage httpRequestMessage)
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            return response;
        }
    }
}