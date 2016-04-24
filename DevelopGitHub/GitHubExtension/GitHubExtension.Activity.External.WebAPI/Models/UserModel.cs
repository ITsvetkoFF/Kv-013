using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class UserModel
    {
        [JsonProperty(PropertyName = GitHubConstants.AvatarUrl)]
        public string AvatarUrl { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.GravatarId)]
        public string GravatarId { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }

        public int Id { get; set; }

        public string Login { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.SiteAdmin)]
        public bool SiteAdmin { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }
    }
}