using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
