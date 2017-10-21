namespace MyDashboard.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Document_Number = c.String(nullable: false, maxLength: 14, fixedLength: true),
                        Email_Address = c.String(nullable: false, maxLength: 60),
                        Name_FirstName = c.String(nullable: false, maxLength: 60),
                        Name_LastName = c.String(maxLength: 60),
                        Phone = c.String(nullable: false, maxLength: 15),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 32, fixedLength: true),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dashboard",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Order = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer", "User_Id", "dbo.User");
            DropForeignKey("dbo.Dashboard", "UserId", "dbo.User");
            DropIndex("dbo.Dashboard", new[] { "UserId" });
            DropIndex("dbo.Customer", new[] { "User_Id" });
            DropTable("dbo.Dashboard");
            DropTable("dbo.User");
            DropTable("dbo.Customer");
        }
    }
}
