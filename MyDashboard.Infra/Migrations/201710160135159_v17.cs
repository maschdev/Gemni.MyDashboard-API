namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v17 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Profile", "Id", c => c.Int(nullable: false, identity: false));
            //AddPrimaryKey("dbo.Profile", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
