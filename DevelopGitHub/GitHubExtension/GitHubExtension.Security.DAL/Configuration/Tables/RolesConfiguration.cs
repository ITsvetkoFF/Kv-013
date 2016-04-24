using System.Data.Entity.ModelConfiguration;

using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class RolesConfiguration : EntityTypeConfiguration<IdentityRole>
    {
        public RolesConfiguration()
        {
            ToTable(TableName);
            HasKey<string>(r => r.Id);
        }

        public string TableName
        {
            get
            {
                return "Roles";
            }
        }
    }
}