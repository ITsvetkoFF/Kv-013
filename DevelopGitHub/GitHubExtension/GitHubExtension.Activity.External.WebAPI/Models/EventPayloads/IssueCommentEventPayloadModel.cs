namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class IssueCommentEventPayloadModel : IPayloadModel
    {
        // The action that was performed on the comment. Currently, can only be "created".
        public string Action { get; set; }

        public IssueCommentModel Comment { get; set; }

        public IssueModel Issue { get; set; }
    }
}