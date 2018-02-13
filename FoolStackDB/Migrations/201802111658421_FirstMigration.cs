namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Capitoloes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        NumeroCapitolo = c.Int(nullable: false),
                        Corso_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Corsoes", t => t.Corso_Id, cascadeDelete: true)
                .Index(t => t.Corso_Id);
            
            CreateTable(
                "dbo.Corsoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        Risorsa = c.String(),
                        Descrizione = c.String(),
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
                "dbo.Messaggios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        Testo = c.String(),
                        DataMessaggio = c.Long(nullable: false),
                        Submitter_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Submitter_Id)
                .Index(t => t.Submitter_Id);
            
            CreateTable(
                "dbo.Rispostas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataRisposta = c.Long(nullable: false),
                        Testo = c.String(),
                        Messaggio_Id = c.Int(nullable: false),
                        Utente_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messaggios", t => t.Messaggio_Id)
                .ForeignKey("dbo.Users", t => t.Utente_Id)
                .Index(t => t.Messaggio_Id)
                .Index(t => t.Utente_Id);
            
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
                "dbo.ProgressoFormaziones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataCompletamento = c.Long(nullable: false),
                        Utente_Id = c.String(nullable: false, maxLength: 128),
                        Capitolo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Utente_Id)
                .ForeignKey("dbo.Capitoloes", t => t.Capitolo_Id)
                .Index(t => t.Utente_Id)
                .Index(t => t.Capitolo_Id);
            
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
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Testo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CorsiUtenti",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Corso_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Corso_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Corsoes", t => t.Corso_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Corso_Id);
            
            CreateTable(
                "dbo.UserEfforts",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Effort_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Effort_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Efforts", t => t.Effort_Id, cascadeDelete: true)
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
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Eventoes", t => t.Evento_Id, cascadeDelete: true)
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
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Eventoes", t => t.Evento_Id, cascadeDelete: true)
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
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tesorerias", t => t.Tesoreria_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Tesoreria_Id);
            
            CreateTable(
                "dbo.CapitoloMessaggios",
                c => new
                    {
                        Capitolo_Id = c.Int(nullable: false),
                        Messaggio_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Capitolo_Id, t.Messaggio_Id })
                .ForeignKey("dbo.Capitoloes", t => t.Capitolo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Messaggios", t => t.Messaggio_Id, cascadeDelete: true)
                .Index(t => t.Capitolo_Id)
                .Index(t => t.Messaggio_Id);
            
            CreateTable(
                "dbo.CapitoloTags",
                c => new
                    {
                        Capitolo_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Capitolo_Id, t.Tag_Id })
                .ForeignKey("dbo.Capitoloes", t => t.Capitolo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Capitolo_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CapitoloTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.CapitoloTags", "Capitolo_Id", "dbo.Capitoloes");
            DropForeignKey("dbo.ProgressoFormaziones", "Capitolo_Id", "dbo.Capitoloes");
            DropForeignKey("dbo.CapitoloMessaggios", "Messaggio_Id", "dbo.Messaggios");
            DropForeignKey("dbo.CapitoloMessaggios", "Capitolo_Id", "dbo.Capitoloes");
            DropForeignKey("dbo.Capitoloes", "Corso_Id", "dbo.Corsoes");
            DropForeignKey("dbo.UserTesorerias", "Tesoreria_Id", "dbo.Tesorerias");
            DropForeignKey("dbo.UserTesorerias", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Rispostas", "Utente_Id", "dbo.Users");
            DropForeignKey("dbo.ProgressoFormaziones", "Utente_Id", "dbo.Users");
            DropForeignKey("dbo.Presenze", "Evento_Id", "dbo.Eventoes");
            DropForeignKey("dbo.Presenze", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Prenotazioni", "Evento_Id", "dbo.Eventoes");
            DropForeignKey("dbo.Prenotazioni", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Messaggios", "Submitter_Id", "dbo.Users");
            DropForeignKey("dbo.Rispostas", "Messaggio_Id", "dbo.Messaggios");
            DropForeignKey("dbo.UserEfforts", "Effort_Id", "dbo.Efforts");
            DropForeignKey("dbo.UserEfforts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.CorsiUtenti", "Corso_Id", "dbo.Corsoes");
            DropForeignKey("dbo.CorsiUtenti", "User_Id", "dbo.Users");
            DropIndex("dbo.CapitoloTags", new[] { "Tag_Id" });
            DropIndex("dbo.CapitoloTags", new[] { "Capitolo_Id" });
            DropIndex("dbo.CapitoloMessaggios", new[] { "Messaggio_Id" });
            DropIndex("dbo.CapitoloMessaggios", new[] { "Capitolo_Id" });
            DropIndex("dbo.UserTesorerias", new[] { "Tesoreria_Id" });
            DropIndex("dbo.UserTesorerias", new[] { "User_Id" });
            DropIndex("dbo.Presenze", new[] { "Evento_Id" });
            DropIndex("dbo.Presenze", new[] { "User_Id" });
            DropIndex("dbo.Prenotazioni", new[] { "Evento_Id" });
            DropIndex("dbo.Prenotazioni", new[] { "User_Id" });
            DropIndex("dbo.UserEfforts", new[] { "Effort_Id" });
            DropIndex("dbo.UserEfforts", new[] { "User_Id" });
            DropIndex("dbo.CorsiUtenti", new[] { "Corso_Id" });
            DropIndex("dbo.CorsiUtenti", new[] { "User_Id" });
            DropIndex("dbo.ProgressoFormaziones", new[] { "Capitolo_Id" });
            DropIndex("dbo.ProgressoFormaziones", new[] { "Utente_Id" });
            DropIndex("dbo.Rispostas", new[] { "Utente_Id" });
            DropIndex("dbo.Rispostas", new[] { "Messaggio_Id" });
            DropIndex("dbo.Messaggios", new[] { "Submitter_Id" });
            DropIndex("dbo.Capitoloes", new[] { "Corso_Id" });
            DropTable("dbo.CapitoloTags");
            DropTable("dbo.CapitoloMessaggios");
            DropTable("dbo.UserTesorerias");
            DropTable("dbo.Presenze");
            DropTable("dbo.Prenotazioni");
            DropTable("dbo.UserEfforts");
            DropTable("dbo.CorsiUtenti");
            DropTable("dbo.Tags");
            DropTable("dbo.Tesorerias");
            DropTable("dbo.ProgressoFormaziones");
            DropTable("dbo.Eventoes");
            DropTable("dbo.Rispostas");
            DropTable("dbo.Messaggios");
            DropTable("dbo.Efforts");
            DropTable("dbo.Users");
            DropTable("dbo.Corsoes");
            DropTable("dbo.Capitoloes");
        }
    }
}
