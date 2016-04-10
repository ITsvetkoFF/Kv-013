using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class PullRequestReviewCommentEventPayloadModel : PayloadModel
    {
        public string Action { get; set; }
        [JsonProperty(PropertyName = GitHubConstants.PullRequest)]
        public PullRequestModel PullRequest { get; set; }

        public PullRequestCommentModel Comment { get; set; }
    }
}
