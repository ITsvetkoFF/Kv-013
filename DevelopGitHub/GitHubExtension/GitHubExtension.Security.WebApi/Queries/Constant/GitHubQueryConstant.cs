namespace GitHubExtension.Security.WebApi.Queries.Constant
{
    public static class GitHubQueryConstant
    {
        public const string GetCollaboratorsRepo = GitHubApi + "repos/{0}/{1}/collaborators?access_token={2}";

        public const string GetRepos = GitHubApi + "user/repos?access_token={0}&affiliation=owner";

        public const string GetUser = GitHubApi + "user?access_token={0}";

        public const string GetUserEmail = GitHubApi + "user/emails?access_token={0}";

        public const string GitHubApi = "https://api.github.com/";
    }
}