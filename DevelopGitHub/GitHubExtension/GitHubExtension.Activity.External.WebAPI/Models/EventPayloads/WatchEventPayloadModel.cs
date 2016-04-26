namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class WatchEventPayloadModel : IPayloadModel
    {
        // The action that was performed. Currently, can only be started.
        public string Action { get; set; }
    }
}