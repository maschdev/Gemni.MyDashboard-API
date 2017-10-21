namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customer", name: "Active", newName: "Enable");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Customer", name: "Enable", newName: "Active");
        }
    }
}
