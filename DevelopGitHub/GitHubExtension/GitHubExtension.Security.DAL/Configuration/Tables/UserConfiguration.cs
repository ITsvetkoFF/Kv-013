using System.Data.Entity.ModelConfiguration;
using GitHubExtension.Security.DAL.Identity;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public string TableName
        {
            get { return "Users"; }
        }
        public UserConfiguration()
        {
            this.ToTable(TableName);
        }
    }
}
