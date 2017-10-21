namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDashboard", "ProfileId", "dbo.Profle");
            DropIndex("dbo.UserDashboard", new[] { "ProfileId" });
            AddColumn("dbo.Profle", "Role", c => c.String());
            DropColumn("dbo.Profle", "Row");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profle", "Row", c => c.String());
            DropColumn("dbo.Profle", "Role");
            CreateIndex("dbo.UserDashboard", "ProfileId");
            AddForeignKey("dbo.UserDashboard", "ProfileId", "dbo.Profle", "Id", cascadeDelete: true);
        }
    }
}
