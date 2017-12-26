namespace FoolStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeneratingManyToManyUserTesoreria : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users");
            DropIndex("dbo.Tesorerias", new[] { "user_Id" });
            CreateTable(
                "dbo.UserTesorerias",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Tesoreria_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Tesoreria_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tesorerias", t => t.Tesoreria_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Tesoreria_Id);
            
            DropColumn("dbo.Tesorerias", "user_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tesorerias", "user_Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.UserTesorerias", "Tesoreria_Id", "dbo.Tesorerias");
            DropForeignKey("dbo.UserTesorerias", "User_Id", "dbo.Users");
            DropIndex("dbo.UserTesorerias", new[] { "Tesoreria_Id" });
            DropIndex("dbo.UserTesorerias", new[] { "User_Id" });
            DropTable("dbo.UserTesorerias");
            CreateIndex("dbo.Tesorerias", "user_Id");
            AddForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users", "Id");
        }
    }
}
