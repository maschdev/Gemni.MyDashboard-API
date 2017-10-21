namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "Profile_Id", c => c.Int());
            CreateIndex("dbo.Customer", "Profile_Id");
            AddForeignKey("dbo.Customer", "Profile_Id", "dbo.Profle", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer", "Profile_Id", "dbo.Profle");
            DropIndex("dbo.Customer", new[] { "Profile_Id" });
            DropColumn("dbo.Customer", "Profile_Id");
        }
    }
}
