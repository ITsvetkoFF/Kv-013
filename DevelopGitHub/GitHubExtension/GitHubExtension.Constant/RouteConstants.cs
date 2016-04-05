using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Constant
{
    public class RouteConstants
    {
        // route prefixes
        public const string ApiRoles = "api/v1/roles";
        public const string ApiAccount = "api/v1/account";
        public const string ApiRepository = "api/v1/repository";

        // static segments
        public const string User = "user";
        public const string Repository = "repository";
        public const string Register = "register";
        public const string ExternalLogin = "externalLogin";
        public const string RegisterExternal = "registerExternal";
        public const string ObtainLocalAccessToken = "obtainLocalAccessToken";
        public const string Collaborators = "collaborators";

        // segments
        public const string Id_int = "{id:int}";
        public const string RepositoryName = "{repositoryName}";
        public const string Id_guid = "{id:guid}";
        public const string UserName = "{username}";
        public const string RepositoryId = "{repositoryId}";
        public const string GitHubId = "{gitHubId}";

        // routes
        public const string AssignRolesToUser = Repository + "/" + RepositoryId + "/" + Collaborators + "/" + GitHubId;
        public const string GetUserByName = User + "/" + UserName;
        public const string GetUser = User + "/" + Id_guid;
        public const string AccountsRegister = Register;
        public const string GetExternalLogin = ExternalLogin;
        public const string GetByIdRepository = Id_int;
        public const string GetRepositoryForCurrentUser = User;
        public const string GetCollaboratorsForRepository = RepositoryName + "/" + Collaborators;
    }
}
