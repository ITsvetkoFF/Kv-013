﻿using GitHubExtension.Infrastructure.Constants;

using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class PullRequestReviewCommentEventPayloadModel : IPayloadModel
    {
        public string Action { get; set; }

        public PullRequestCommentModel Comment { get; set; }

        [JsonProperty(PropertyName = GitHubConstants.PullRequest)]
        public PullRequestModel PullRequest { get; set; }
    }
}