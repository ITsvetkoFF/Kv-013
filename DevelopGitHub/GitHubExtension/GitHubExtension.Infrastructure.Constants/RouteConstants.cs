namespace GitHubExtension.Infrastructure.Constants
{
    public static class RouteConstants
    {
        // route prefixes
        public const string ApiRoles = "api/roles";
        public const string ApiAccount = "api/account";
        public const string ApiRepository = "api/repos";
        public const string ApiUser = "api/user";

        // static segments
        public const string Home = "http://localhost:50859/";
        public const string Logout = "logout";
        public const string User = "user";
        public const string Repository = "/repos";
        public const string Register = "register";
        public const string ExternalLogin = "externalLogin";
        public const string RegisterExternal = "registerExternal";
        public const string ObtainLocalAccessToken = "obtainLocalAccessToken";
        public const string Collaborators = "/collaborators";

        // segments
        public const string Id_int = "/{id:int}";
        public const string RepositoryName = "/{repoName}";
        public const string Id_guid = "/{id:guid}";
        public const string UserName = "/{username}";
        public const string RepositoryId = "/{repoId}";
        public const string GitHubId = "/{gitHubId}";

        // routes
        public const string AccountLogout = Logout;
        public const string RedirectHome = Home;
        public const string AssignRolesToUser = ApiRepository + RepositoryId + Collaborators + GitHubId;
        public const string GetUserByName = User + UserName;
        public const string GetUser = User + Id_guid;
        public const string AccountsRegister = Register;
        public const string GetExternalLogin = ExternalLogin;
        public const string GetByIdRepository = ApiRepository + Id_int;
        public const string GetRepositoryForCurrentUser = ApiUser + Repository;
        public const string GetCollaboratorsForRepository = ApiRepository + RepositoryName + Collaborators;
    }
}
