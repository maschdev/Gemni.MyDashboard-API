namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customer", "ProfileId", "dbo.Profle");
            DropIndex("dbo.Customer", new[] { "ProfileId" });
            AddColumn("dbo.UserDashboard", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserDashboard", "ProfileId");
            AddForeignKey("dbo.UserDashboard", "ProfileId", "dbo.Profle", "Id", cascadeDelete: true);
            DropColumn("dbo.Customer", "ProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "ProfileId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserDashboard", "ProfileId", "dbo.Profle");
            DropIndex("dbo.UserDashboard", new[] { "ProfileId" });
            DropColumn("dbo.UserDashboard", "ProfileId");
            CreateIndex("dbo.Customer", "ProfileId");
            AddForeignKey("dbo.Customer", "ProfileId", "dbo.Profle", "Id", cascadeDelete: true);
        }
    }
}
