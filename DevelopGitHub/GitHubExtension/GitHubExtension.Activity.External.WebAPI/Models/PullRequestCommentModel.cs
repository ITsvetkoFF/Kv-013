using System;
using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class PullRequestCommentModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
        public UserModel User { get; set; }
        public string Body { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
    }
}