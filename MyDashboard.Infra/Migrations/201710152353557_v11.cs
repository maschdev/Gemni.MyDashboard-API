namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customer", name: "Address", newName: "Email");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Customer", name: "Email", newName: "Address");
        }
    }
}
