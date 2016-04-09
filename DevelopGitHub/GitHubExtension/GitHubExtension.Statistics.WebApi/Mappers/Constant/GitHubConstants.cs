using System.Collections.Generic;

namespace GitHubExtension.Statistics.WebApi.Mappers.Constant
{
    public class GitHubConstants
    {
        public static readonly Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>()
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36" }
        };
    }
}
