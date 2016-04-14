using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class UserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public UserRoleConfiguration()
        {
            this.ToTable(TableName);
            HasKey(r => new { r.RoleId, r.UserId });
        }

        public string TableName
        {
            get { return "UserRoles"; }
        }
    }
}
