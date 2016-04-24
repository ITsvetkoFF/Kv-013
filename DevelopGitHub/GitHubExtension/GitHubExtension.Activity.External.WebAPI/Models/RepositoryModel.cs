using System;

using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class RepositoryModel
    {
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(GitHubConstants.DefaultBranch)]
        public string DefaultBranch { get; set; }

        public string Description { get; set; }

        public bool Fork { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.FullName)]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public UserModel Owner { get; set; }

        public bool Private { get; set; }

        public bool Public { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.PushedAt)]
        public DateTime PushedAt { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }

        public string Url { get; set; }
    }
}