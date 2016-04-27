namespace GitHubExtension.Security.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddField_IsMailVisible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsMailVisible", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "ShowEmailEnabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ShowEmailEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "IsMailVisible");
        }
    }
}
