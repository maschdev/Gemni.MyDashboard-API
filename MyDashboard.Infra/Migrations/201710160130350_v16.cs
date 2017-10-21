namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v16 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Profile");
            AlterColumn("dbo.Profile", "Id", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.Profile", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Profile");
            AlterColumn("dbo.Profile", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Profile", "Id");
        }
    }
}
