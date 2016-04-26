using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class DownloadModel
    {
        [JsonProperty(PropertyName = GitHubConstants.ContentType)]
        public string ContentType { get; set; }

        public string Description { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.DownloadCount)]
        public int DownloadCount { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string Url { get; set; }
    }
}