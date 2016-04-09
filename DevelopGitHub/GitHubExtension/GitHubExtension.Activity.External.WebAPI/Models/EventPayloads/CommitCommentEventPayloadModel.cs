namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class CommitCommentEventPayloadModel : PayloadModel
    {
        // Currently only created
        public string Action { get; set; }
        public CommitCommentModel Comment { get; set; }
    }
}
