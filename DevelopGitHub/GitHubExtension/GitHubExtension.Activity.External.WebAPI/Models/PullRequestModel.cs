using System;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class PullRequestModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        public int Number { get; set; }
        public string State { get; set; }
        public bool Locked { get; set; }
        public string Title { get; set; }
        public UserModel User { get; set; }
        public string Body { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UpdatedAt)]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.ClosedAt)]
        public DateTime? ClosedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.MergedAt)]
        public DateTime? MergedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.MergeCommitSha)]
        public string MergeCommitSha { get; set; }
        public UserModel Assignee { get; set; }
        public bool Merged { get; set; }

        //The value of the mergeable attribute can be true, false, or null. If the value is null, this means that the mergeability hasn't been computed yet, and a background job was started to compute it
        public bool? Mergeable { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.MergeableState)]
        public string MergeableState { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.MergedBy)]
        public UserModel MergedBy { get; set; }
        public int Comments { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.ReviewComments)]
        public int ReviewComments { get; set; }
        public int Commits { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.ChangedFiles)]
        public int ChangedFiles { get; set; }
    }
}