using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubExtension.Templates.WebApi.Constants
{
    public class RouteConstants
    {
        public const string GetGitHubTemplatesRoute = "api/templates";
        public const string PullRequestTemplate = "pullRequestTemplate";
        public const string IssueTemplate = "issueTemplate";
        public const string PathToPullRequestTemplate = ".github/PULL_REQUEST_TEMPLATE.md";
        public const string PathToIssueTemplate = ".github/ISSUE_TEMPLATE.md";
        public const string RouteToRepository = "https://api.github.com/repos/";
        public const string Contents = "/contents/";


    }
}