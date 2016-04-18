using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class GistModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public UserModel Owner { get; set; }
        public bool Truncated { get; set; }
        public int Comments { get; set; }
        // Also it has list of files that are included in gist, but we don't need them for now

        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public string CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public string UpdatedAt { get; set; }
    }
}