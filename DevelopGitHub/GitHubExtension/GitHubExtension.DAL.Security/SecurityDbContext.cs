using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Domain.Interfaces;
using GitHubExtension.WebApi.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Domain
{
    public class SecurityDbContext : IdentityDbContext<IdentityUser>, ISecurityDbContext
    {
        public SecurityDbContext()
            : base("SecurityDataBase1") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
