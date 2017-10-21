namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Row = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customer", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customer", "ProfileId");
            AddForeignKey("dbo.Customer", "ProfileId", "dbo.Profle", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer", "ProfileId", "dbo.Profle");
            DropIndex("dbo.Customer", new[] { "ProfileId" });
            DropColumn("dbo.Customer", "ProfileId");
            DropTable("dbo.Profle");
        }
    }
}
