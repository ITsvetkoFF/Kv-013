using System;

namespace GitHubExtension.Infrastructure.Constants
{
    public static class RepositoryValidationConstants
    {
        public const string UserDoesNotHaveRepository = "User with id {0} don't have a repository with id {1} in system";

        public const string CurrentProjectUpdateFailed = "Failed to update current project";

        public const string CurrentProject = "CurrentProject";

        public const string Repository = "Repository";
    }
}
