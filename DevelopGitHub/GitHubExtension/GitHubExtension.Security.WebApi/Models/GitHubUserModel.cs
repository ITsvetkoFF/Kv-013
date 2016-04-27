using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Security.WebApi.Models
{
    public class GitHubUserModel
    {
        public string Email { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.Id)]
        public int GitHubId { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string AvatarUrl { get; set; }
    }
}