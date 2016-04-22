using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Security.DAL.Identity;

namespace GitHubExtension.Security.WebApi.Queries.Interfaces
{
    public interface ISecurityContextQuery
    {
        IQueryable<SecurityRole> SecurityRoles { get; }
    }
}
