using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Constant
{
    public class RouteConstant
    {
        // route prefixes
        public const string apiRoles = "api/v1/roles";
        public const string apiAccount = "api/v1/account";
        public const string apiRepository = "api/v1/repository";

        // static segments
        public const string user = "user";
        public const string repository = "repository";
        public const string register = "register";
        public const string externalLogin = "externalLogin";
        public const string registerExternal = "registerExternal";
        public const string obtainLocalAccessToken = "obtainLocalAccessToken";
        public const string collaborators = "collaborators";

        // segments
        public const string id_int = "{id:int}";
        public const string repositoryName = "{repositoryName}";
        public const string id_guid = "{id:guid}";
        public const string userName = "{username}";
        public const string repositoryId = "{repositoryId}";
        public const string gitHubId = "{gitHubId}";

        // routes
        public const string assignRolesToUser = repository + "/" + repositoryId + "/" + collaborators + "/" + gitHubId;
        public const string getUserByName = user + "/" + userName;
        public const string getUser = user + "/" + id_guid;
        public const string accountsRegister = register;
        public const string getExternalLogin = externalLogin;
        public const string getByIdRepository = id_int;
        public const string getRepositoryForCurrentUser = user;
        public const string getCollaboratorsForRepository = repositoryName + "/" + collaborators;
    }
}
