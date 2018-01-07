namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Efforts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataCreazione = c.Long(nullable: false),
                        DataChiusura = c.Long(nullable: false),
                        Stato = c.String(),
                        Titolo = c.String(),
                        Descrizione = c.String(),
                        Priorita = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Eventoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataEvento = c.Long(nullable: false),
                        Titolo = c.String(nullable: false, maxLength: 256),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tesorerias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataOperazione = c.Long(nullable: false),
                        Operazione = c.String(),
                        Quota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserEfforts",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Effort_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Effort_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.Efforts", t => t.Effort_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Effort_Id);
            
            CreateTable(
                "dbo.Prenotazioni",
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
            
            CreateTable(
                "dbo.UserTesorerias",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Tesoreria_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Tesoreria_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.Tesorerias", t => t.Tesoreria_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Tesoreria_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTesorerias", "Tesoreria_Id", "dbo.Tesorerias");
            DropForeignKey("dbo.UserTesorerias", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Presenze", "Evento_Id", "dbo.Eventoes");
            DropForeignKey("dbo.Presenze", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Prenotazioni", "Evento_Id", "dbo.Eventoes");
            DropForeignKey("dbo.Prenotazioni", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserEfforts", "Effort_Id", "dbo.Efforts");
            DropForeignKey("dbo.UserEfforts", "User_Id", "dbo.Users");
            DropIndex("dbo.UserTesorerias", new[] { "Tesoreria_Id" });
            DropIndex("dbo.UserTesorerias", new[] { "User_Id" });
            DropIndex("dbo.Presenze", new[] { "Evento_Id" });
            DropIndex("dbo.Presenze", new[] { "User_Id" });
            DropIndex("dbo.Prenotazioni", new[] { "Evento_Id" });
            DropIndex("dbo.Prenotazioni", new[] { "User_Id" });
            DropIndex("dbo.UserEfforts", new[] { "Effort_Id" });
            DropIndex("dbo.UserEfforts", new[] { "User_Id" });
            DropTable("dbo.UserTesorerias");
            DropTable("dbo.Presenze");
            DropTable("dbo.Prenotazioni");
            DropTable("dbo.UserEfforts");
            DropTable("dbo.Tesorerias");
            DropTable("dbo.Eventoes");
            DropTable("dbo.Users");
            DropTable("dbo.Efforts");
        }
    }
}
