namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCorsiUtentiRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CorsiUtenti",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Corso_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Corso_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.Corsoes", t => t.Corso_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Corso_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CorsiUtenti", "Corso_Id", "dbo.Corsoes");
            DropForeignKey("dbo.CorsiUtenti", "User_Id", "dbo.Users");
            DropIndex("dbo.CorsiUtenti", new[] { "Corso_Id" });
            DropIndex("dbo.CorsiUtenti", new[] { "User_Id" });
            DropTable("dbo.CorsiUtenti");
        }
    }
}
