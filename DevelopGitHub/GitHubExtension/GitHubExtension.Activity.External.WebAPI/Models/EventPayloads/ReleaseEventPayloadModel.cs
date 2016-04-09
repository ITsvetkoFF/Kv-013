namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class ReleaseEventPayloadModel : PayloadModel
    {
        // The action that was performed. Currently, can only be "published".
        public string Action { get; set; }
        public ReleaseModel Release { get; set; }
    }
}
