using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class RolesConfiguration : EntityTypeConfiguration<IdentityRole>
    {
        public string TableName
        {
            get { return "Roles"; }
        }
        public RolesConfiguration()
        {
            this.ToTable(TableName);
            this.HasKey<string>(r => r.Id);
        }
    }
}
