namespace FoolStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users");
            DropIndex("dbo.Tesorerias", new[] { "user_Id" });
            AlterColumn("dbo.Tesorerias", "user_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Tesorerias", "user_Id");
            AddForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users");
            DropIndex("dbo.Tesorerias", new[] { "user_Id" });
            AlterColumn("dbo.Tesorerias", "user_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tesorerias", "user_Id");
            AddForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users", "Id");
        }
    }
}
