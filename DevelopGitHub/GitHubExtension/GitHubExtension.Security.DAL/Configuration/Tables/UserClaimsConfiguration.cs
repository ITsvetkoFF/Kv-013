using System.Data.Entity.ModelConfiguration;

using Microsoft.AspNet.Identity.EntityFramework;

namespace GitHubExtension.Security.DAL.Configuration.Tables
{
    public class UserClaimsConfiguration : EntityTypeConfiguration<IdentityUserClaim>
    {
        public UserClaimsConfiguration()
        {
            ToTable(TableName);
        }

        public string TableName
        {
            get
            {
                return "UserClaims";
            }
        }
    }
}