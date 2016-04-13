namespace GitHubExtension.Statistics.WebApi.Constant
{
    public static class StatisticsRouteConstants
    {
        public const string User = "api/user/stats";
        public const string GetUserCommits = User + "/" + "commits";
        public const string GetRepoByName = GetUserCommits + "/" + "{name}";
        public const string UserInfo = User + "/info";
        public const string GetFollowers = UserInfo + "/followers";
        public const string GetFollowing = UserInfo + "/following";
        public const string GetRepositoriesCount = UserInfo + "/repositories-count";
        public const string GetActivityMonths = User + "/activity-months";
        public const string GetRepositories = User + "/repositories";
        public const string GetCommitsRepositories = User + "/repositories/commits";
        public const string GetGroupCommits = User + "/repositories/group-commits";
    }
}
