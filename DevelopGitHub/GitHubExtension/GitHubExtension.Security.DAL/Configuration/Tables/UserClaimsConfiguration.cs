using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
