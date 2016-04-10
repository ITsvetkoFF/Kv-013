using System;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class IssueModel
    {
        public string Url { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public bool Locked { get; set; }
        public UserModel Assignee { get; set; }
        public UserModel User { get; set; }
        public int Comments { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.ClosedAt)]
        public DateTime? ClosedAt { get; set; }
        public string Body { get; set; }
    }
}