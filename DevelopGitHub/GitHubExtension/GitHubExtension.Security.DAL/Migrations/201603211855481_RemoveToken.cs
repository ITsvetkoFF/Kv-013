namespace GitHubExtension.Security.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveToken : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Token");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Token", c => c.String());
        }
    }
}
