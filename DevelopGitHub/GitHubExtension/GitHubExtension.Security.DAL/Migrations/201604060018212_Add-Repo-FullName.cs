namespace GitHubExtension.Security.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRepoFullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Repositories", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Repositories", "FullName");
        }
    }
}
