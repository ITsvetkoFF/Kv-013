using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class DeleteEventPayloadModel : PayloadModel
    {
        //represents deleted branch or tag.
        public string Ref { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.RefType)]
        public string RefType { get; set; }
    }
}
