using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class ActorModel
    {
        [JsonProperty(PropertyName = GitHubConstants.AvatarUrl)]
        public string AvatarUrl { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.GravatarId)]
        public string GravatarId { get; set; }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Url { get; set; }
    }
}