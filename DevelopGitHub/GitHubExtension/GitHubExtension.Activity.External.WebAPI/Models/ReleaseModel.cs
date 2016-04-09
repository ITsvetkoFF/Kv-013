using System;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    public class ReleaseModel
    {
        public string Url { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.UploadUrl)]
        public string UploadUrl { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.HtmlUrl)]
        public string HtmlUrl { get; set; }
        public int Id { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.TagName)]
        public string TagName { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.TargetCommitish)]
        public string TargetCommitish { get; set; }
        public string Name { get; set; }
        public bool Draft { get; set; }
        public UserModel Author { get; set; }
        public bool Prerelease { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.PublishedAt)]
        public DateTime PublishedAt { get; set; }
        public string Body { get; set; }
    }
}