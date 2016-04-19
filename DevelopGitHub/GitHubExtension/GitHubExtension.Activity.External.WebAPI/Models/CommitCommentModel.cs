using System;
using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class CommitCommentModel
    {
        public string Url { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        public int Id { get; set; }
        public UserModel User { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CommitId)]
        public string CommitId { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }
        public string Body { get; set; }
    }
}