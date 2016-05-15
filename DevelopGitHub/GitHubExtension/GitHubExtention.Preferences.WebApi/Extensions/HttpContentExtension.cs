using System.Net.Http;
using System.Text.RegularExpressions;

namespace GitHubExtention.Preferences.WebApi.Extensions
{
    public static class HttpContentExtension
    {
        private static string pattern = "(\\.\\w+)\\\"$";

        public static string GetFileExtension(this HttpContent content)
        {
            Match extensionMatch = Regex.Match(content.Headers.ContentDisposition.FileName, pattern);
            return extensionMatch.Groups[1].Value;
        }
    }
}
