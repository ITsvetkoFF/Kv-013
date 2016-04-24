namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class IssuesEventPayloadModel : IPayloadModel
    {
        // The action that was performed. Can be one of "assigned", "unassigned", "labeled", "unlabeled", "opened", "closed", or "reopened".
        public string Action { get; set; }

        public UserModel Assignee { get; set; }

        public IssueModel Issue { get; set; }
    }
}