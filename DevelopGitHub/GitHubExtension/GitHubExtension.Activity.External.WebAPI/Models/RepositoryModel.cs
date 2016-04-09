using System;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class RepositoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.FullName)]
        public string FullName { get; set; }
        public UserModel Owner { get; set; }
        public bool Private { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        public string Description { get; set; }
        public bool Fork { get; set; }
        public string Url { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.PushedAt)]
        public DateTime PushedAt { get; set; }
        [JsonProperty(GitHubConstants.DefaultBranch)]
        public string DefaultBranch { get; set; }
        public bool Public { get; set; }
    }
}