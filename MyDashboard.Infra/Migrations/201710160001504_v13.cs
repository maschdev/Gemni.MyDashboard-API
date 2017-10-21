namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customer", "Profile_Id", "dbo.Profle");
            DropIndex("dbo.Customer", new[] { "Profile_Id" });
            DropColumn("dbo.Customer", "Profile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "Profile_Id", c => c.Int());
            CreateIndex("dbo.Customer", "Profile_Id");
            AddForeignKey("dbo.Customer", "Profile_Id", "dbo.Profle", "Id");
        }
    }
}
