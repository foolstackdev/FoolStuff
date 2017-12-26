namespace FoolStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCascadeDeleteUserTesoreria : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users");
            AddForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users");
            AddForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
