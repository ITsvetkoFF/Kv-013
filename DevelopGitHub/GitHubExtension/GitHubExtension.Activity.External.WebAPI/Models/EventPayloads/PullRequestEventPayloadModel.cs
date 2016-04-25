using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class PullRequestEventPayloadModel : IPayloadModel
    {
        // The action that was performed
        // Can be one of "assigned", "unassigned", "labeled", "unlabeled", "opened", "closed", or "reopened", or "synchronize"
        // If the action is "closed" and the merged key is false, the pull request was closed with unmerged commits
        // If the action is "closed" and the merged key is true, the pull request was merged.
        public string Action { get; set; }

        public int Number { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.PullRequest)]
        public PullRequestModel PullRequest { get; set; }
    }
}