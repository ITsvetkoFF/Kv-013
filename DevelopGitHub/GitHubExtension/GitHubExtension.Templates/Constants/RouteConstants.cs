namespace GitHubExtension.Templates.Constants
{
    public static class RouteTemplatesConstants
    {
        public const string PullRequestTemplate = GetGitHubTemplatesRoute + PullRequest;
        public const string IssueTemplate = GetGitHubTemplatesRoute + Issue;
        public const string GetPullRequests = GetGitHubTemplatesRoute + PullRequestFromDb;
        public const string GetIssueTemplateCategories = GetGitHubTemplatesRoute + Categories;
        public const string GetIssueTemplateByCategoryId = GetGitHubTemplatesRoute + Categories + CategoryId;
        public const string GetGitHubTemplatesRoute = "api/templates";
        public const string PullRequest = "/pull-request";
        public const string Issue = "/issue";
        public const string PullRequestFromDb = "/pr";
        public const string Categories = "/categories";
        public const string CategoryId = "/{id}";
    }
}