using System;
using GitHubExtension.Activity.External.WebAPI.Models.EventPayloads;
using GitHubExtension.Infrastructure.Constants;
using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models
{
    [JsonConverter(typeof(EventModelJsonConverter))]
    public class GitHubEventModel
    {
        public string Type { get; set; }
        public IPayloadModel Payload { get; set; }
        public ActorModel Actor { get; set; }
        public RepositoryShortModel Repo { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.CreatedAt)]
        public DateTime CreatedAt { get; set; }
        public string Id { get; set; }
    }
}
