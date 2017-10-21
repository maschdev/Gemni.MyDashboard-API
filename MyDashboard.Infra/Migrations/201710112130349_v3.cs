namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customer", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Customer", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Customer", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Customer", name: "UserId", newName: "User_Id");
        }
    }
}
