namespace GitHubExtension.Constant
{
    public static class RouteConstants
    {
        // route prefixes
        public const string ApiRoles = "api/roles";
        public const string ApiAccount = "api/account";
        public const string ApiRepository = "api/repos";
        public const string ApiUser = "api/user";

        // static segments
        public const string User = "user";
        public const string Repository = "/repos";
        public const string Register = "register";
        public const string ExternalLogin = "externalLogin";
        public const string RegisterExternal = "registerExternal";
        public const string ObtainLocalAccessToken = "obtainLocalAccessToken";
        public const string Collaborators = "/collaborators";

        // segments
        public const string IdInt = "/{id:int}";
        public const string RepositoryName = "/{repoName}";
        public const string IdGuid = "/{id:guid}";
        public const string UserName = "/{username}";
        public const string RepositoryId = "/{repoId}";
        public const string GitHubId = "/{gitHubId}";

        // routes
        public const string AssignRolesToUser = ApiRepository + RepositoryId + Collaborators + GitHubId;
        public const string GetUserByName = User + UserName;
        public const string GetUser = User + IdGuid;
        public const string AccountsRegister = Register;
        public const string GetExternalLogin = ExternalLogin;
        public const string GetByIdRepository = ApiRepository + IdInt;
        public const string GetRepositoryForCurrentUser = ApiUser + Repository;
        public const string GetCollaboratorsForRepository = ApiRepository + RepositoryName + Collaborators;
    }
}
