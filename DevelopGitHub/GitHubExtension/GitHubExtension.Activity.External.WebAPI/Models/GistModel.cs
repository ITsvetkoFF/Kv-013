using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class GistModel
    {
        public int Comments { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public string CreatedAt { get; set; }

        public string Description { get; set; }

        // Also it has list of files that are included in gist, but we don't need them for now
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }

        public string Id { get; set; }

        public UserModel Owner { get; set; }

        public bool Public { get; set; }

        public bool Truncated { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public string UpdatedAt { get; set; }

        public string Url { get; set; }
    }
}