using System;
using GitHubExtension.Activity.External.WebAPI.Models.EventPayloads;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    [JsonConverter(typeof(EventModelConverter))]
    public class GitHubEventModel
    {
        public string Type { get; set; }
        public PayloadModel Payload { get; set; }
        public ActorModel Actor { get; set; }
        public RepositoryShortModel Repo { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        public string Id { get; set; }
    }
}
