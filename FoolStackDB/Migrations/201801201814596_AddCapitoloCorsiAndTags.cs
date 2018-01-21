namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCapitoloCorsiAndTags : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RispostaMessaggios", newName: "MessaggioRispostas");
            DropPrimaryKey("dbo.MessaggioRispostas");
            CreateTable(
                "dbo.Capitoloes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        Corso_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Corsoes", t => t.Corso_Id, cascadeDelete: false)
                .Index(t => t.Corso_Id);
            
            CreateTable(
                "dbo.Corsoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        Risorsa = c.String(),
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
                "dbo.CapitoloMessaggios",
                c => new
                    {
                        Capitolo_Id = c.Int(nullable: false),
                        Messaggio_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Capitolo_Id, t.Messaggio_Id })
                .ForeignKey("dbo.Capitoloes", t => t.Capitolo_Id, cascadeDelete: false)
                .ForeignKey("dbo.Messaggios", t => t.Messaggio_Id, cascadeDelete: false)
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
                .ForeignKey("dbo.Capitoloes", t => t.Capitolo_Id, cascadeDelete: false)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: false)
                .Index(t => t.Capitolo_Id)
                .Index(t => t.Tag_Id);
            
            AddPrimaryKey("dbo.MessaggioRispostas", new[] { "Messaggio_Id", "Risposta_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CapitoloTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.CapitoloTags", "Capitolo_Id", "dbo.Capitoloes");
            DropForeignKey("dbo.CapitoloMessaggios", "Messaggio_Id", "dbo.Messaggios");
            DropForeignKey("dbo.CapitoloMessaggios", "Capitolo_Id", "dbo.Capitoloes");
            DropForeignKey("dbo.Capitoloes", "Corso_Id", "dbo.Corsoes");
            DropIndex("dbo.CapitoloTags", new[] { "Tag_Id" });
            DropIndex("dbo.CapitoloTags", new[] { "Capitolo_Id" });
            DropIndex("dbo.CapitoloMessaggios", new[] { "Messaggio_Id" });
            DropIndex("dbo.CapitoloMessaggios", new[] { "Capitolo_Id" });
            DropIndex("dbo.Capitoloes", new[] { "Corso_Id" });
            DropPrimaryKey("dbo.MessaggioRispostas");
            DropTable("dbo.CapitoloTags");
            DropTable("dbo.CapitoloMessaggios");
            DropTable("dbo.Tags");
            DropTable("dbo.Corsoes");
            DropTable("dbo.Capitoloes");
            AddPrimaryKey("dbo.MessaggioRispostas", new[] { "Risposta_Id", "Messaggio_Id" });
            RenameTable(name: "dbo.MessaggioRispostas", newName: "RispostaMessaggios");
        }
    }
}
