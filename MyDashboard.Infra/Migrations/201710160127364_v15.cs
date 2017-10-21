namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Profle", newName: "Profile");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Profile", newName: "Profle");
        }
    }
}
