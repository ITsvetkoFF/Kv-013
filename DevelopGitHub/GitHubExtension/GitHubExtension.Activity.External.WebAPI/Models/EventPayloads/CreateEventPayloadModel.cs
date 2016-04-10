using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class CreateEventPayloadModel : PayloadModel
    {
        public string Ref { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.RefType)]
        public string RefType { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.MasterBranch)]
        public string MasterBranch { get; set; }
        public string Description { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.PusherType)]
        public string PusherType { get; set; }
    }
}
