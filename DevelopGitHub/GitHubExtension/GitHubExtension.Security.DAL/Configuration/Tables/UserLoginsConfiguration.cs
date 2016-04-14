using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class UserLoginsConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public UserLoginsConfiguration()
        {
            this.ToTable(TableName);
            HasKey<string>(l => l.UserId);
        }

        public string TableName
        {
            get { return "UserLogins"; }
        }
    }
}
