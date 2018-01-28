namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRisposteToMessages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessaggioRispostas", "Messaggio_Id", "dbo.Messaggios");
            DropForeignKey("dbo.MessaggioRispostas", "Risposta_Id", "dbo.Rispostas");
            DropForeignKey("dbo.UserRispostas", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRispostas", "Risposta_Id", "dbo.Rispostas");
            DropIndex("dbo.MessaggioRispostas", new[] { "Messaggio_Id" });
            DropIndex("dbo.MessaggioRispostas", new[] { "Risposta_Id" });
            DropIndex("dbo.UserRispostas", new[] { "User_Id" });
            DropIndex("dbo.UserRispostas", new[] { "Risposta_Id" });
            AddColumn("dbo.Rispostas", "Messaggio_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Rispostas", "Utente_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Rispostas", "Messaggio_Id");
            CreateIndex("dbo.Rispostas", "Utente_Id");
            AddForeignKey("dbo.Rispostas", "Messaggio_Id", "dbo.Messaggios", "Id");
            AddForeignKey("dbo.Rispostas", "Utente_Id", "dbo.Users", "Id");
            DropTable("dbo.MessaggioRispostas");
            DropTable("dbo.UserRispostas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRispostas",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Risposta_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Risposta_Id });
            
            CreateTable(
                "dbo.MessaggioRispostas",
                c => new
                    {
                        Messaggio_Id = c.Int(nullable: false),
                        Risposta_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Messaggio_Id, t.Risposta_Id });
            
            DropForeignKey("dbo.Rispostas", "Utente_Id", "dbo.Users");
            DropForeignKey("dbo.Rispostas", "Messaggio_Id", "dbo.Messaggios");
            DropIndex("dbo.Rispostas", new[] { "Utente_Id" });
            DropIndex("dbo.Rispostas", new[] { "Messaggio_Id" });
            DropColumn("dbo.Rispostas", "Utente_Id");
            DropColumn("dbo.Rispostas", "Messaggio_Id");
            CreateIndex("dbo.UserRispostas", "Risposta_Id");
            CreateIndex("dbo.UserRispostas", "User_Id");
            CreateIndex("dbo.MessaggioRispostas", "Risposta_Id");
            CreateIndex("dbo.MessaggioRispostas", "Messaggio_Id");
            AddForeignKey("dbo.UserRispostas", "Risposta_Id", "dbo.Rispostas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRispostas", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MessaggioRispostas", "Risposta_Id", "dbo.Rispostas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MessaggioRispostas", "Messaggio_Id", "dbo.Messaggios", "Id", cascadeDelete: true);
        }
    }
}
