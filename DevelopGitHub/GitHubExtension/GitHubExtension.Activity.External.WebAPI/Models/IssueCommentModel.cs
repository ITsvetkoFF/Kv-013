using System;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class IssueCommentModel
    {
        public string Url { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.IssueUrl)]
        public string IssueUrl { get; set; }
        public int Id { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }
        public string Body { get; set; }
    }
}