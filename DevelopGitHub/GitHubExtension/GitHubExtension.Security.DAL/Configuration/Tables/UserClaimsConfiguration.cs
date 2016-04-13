using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class UserClaimsConfiguration : EntityTypeConfiguration<IdentityUserClaim>
    {
        public string TableName
        {
            get { return "UserClaims"; }
        }
        public UserClaimsConfiguration()
        {
            this.ToTable(TableName);
        }
    }
}
