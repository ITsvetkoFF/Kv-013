namespace GitHubExtension.Notes.WebApi.Constants
{
    public static class ValidationConstants
    {
        public const int MaxBodyLength = 2048;

        // note body validators
        public const int MinBodyLength = 5;

        public const string UserId = "UserId";

        public const string UserIdError = "User not authorized";
    }
}