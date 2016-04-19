using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class UserModel
    {
        public string Login { get; set; }
        public int Id { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.AvatarUrl)]
        public string AvatarUrl { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.GravatarId)]
        public string GravatarId { get; set; }
        public string Url { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        public string Type { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.SiteAdmin)]
        public bool SiteAdmin { get; set; }
    }
}