namespace GitHubExtension.Statistics.WebApi.Queries.Constant
{
    public static class GitHubQueryConstant
    {
        public const string GitHubApi = "https://api.github.com/";
        public const string GetRepositoryCommits = GitHubApi + "repos/{0}/{1}/stats/participation?access_token={2}";
        public const string GetRepositories = GitHubApi + "users/{0}/repos?access_token={1}";
        public const string GetFollowers = GitHubApi + "users/{0}/followers?access_token={1}";
        public const string GetFollowing = GitHubApi + "users/{0}/following?access_token={1}";
    }
}
