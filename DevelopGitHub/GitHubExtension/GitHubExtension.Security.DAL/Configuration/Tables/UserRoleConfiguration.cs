using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class UserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public string TableName
        {
            get { return "UserRoles"; }
        }

        public UserRoleConfiguration()
        {
            this.ToTable(TableName);
            HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
