using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return _securityContext.SecurityRoles; }
        }
    }
}
