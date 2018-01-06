namespace FoolStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPresenzeAndPrenotazioni : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserEventoes", newName: "Prenotazioni");
            CreateTable(
                "dbo.Presenze",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Evento_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Evento_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.Eventoes", t => t.Evento_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Evento_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Presenze", "Evento_Id", "dbo.Eventoes");
            DropForeignKey("dbo.Presenze", "User_Id", "dbo.Users");
            DropIndex("dbo.Presenze", new[] { "Evento_Id" });
            DropIndex("dbo.Presenze", new[] { "User_Id" });
            DropTable("dbo.Presenze");
            RenameTable(name: "dbo.Prenotazioni", newName: "UserEventoes");
        }
    }
}
