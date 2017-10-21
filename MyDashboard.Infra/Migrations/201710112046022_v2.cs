namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customer", name: "Document_Number", newName: "Number");
            RenameColumn(table: "dbo.Customer", name: "Email_Address", newName: "Address");
            RenameColumn(table: "dbo.Customer", name: "Name_FirstName", newName: "FirstName");
            RenameColumn(table: "dbo.Customer", name: "Name_LastName", newName: "LastName");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Customer", name: "LastName", newName: "Name_LastName");
            RenameColumn(table: "dbo.Customer", name: "FirstName", newName: "Name_FirstName");
            RenameColumn(table: "dbo.Customer", name: "Address", newName: "Email_Address");
            RenameColumn(table: "dbo.Customer", name: "Number", newName: "Document_Number");
        }
    }
}
