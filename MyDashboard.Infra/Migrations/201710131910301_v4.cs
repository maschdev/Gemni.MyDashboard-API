namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.User", newName: "UserDashboard");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserDashboard", newName: "User");
        }
    }
}
