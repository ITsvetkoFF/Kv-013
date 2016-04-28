using System.Linq;

using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.DAL.Interfaces;
using GitHubExtension.Security.WebApi.Queries.Interfaces;

namespace GitHubExtension.Security.WebApi.Queries.Implementations
{
    public class SecurityContextQuery : ISecurityContextQuery
    {
        private readonly ISecurityContext _securityContext;

        public SecurityContextQuery(ISecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        public IQueryable<SecurityRole> SecurityRoles
        {
            get
            {
                return _securityContext.SecurityRoles;
            }
        }

        public IQueryable<UserRepositoryRole> UserRepositoryRoles
        {
            get
            {
                return _securityContext.UserRepository;
            }
        }

        public IQueryable<Repository> Repositories
        {
            get
            {
                return _securityContext.Repositories;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                return _securityContext.Users;
            }
        }
    }
}