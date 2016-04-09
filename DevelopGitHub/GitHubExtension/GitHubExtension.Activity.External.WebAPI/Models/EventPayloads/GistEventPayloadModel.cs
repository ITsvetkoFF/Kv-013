namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    // Events of this type are no longer created, but it's possible that they exist in timelines of some users.
    public class GistEventPayloadModel : PayloadModel
    {
        // The action that was performed. Can be "create" or "update"
        public string Action { get; set; }
        public GistModel Gist { get; set; }
    }
}
