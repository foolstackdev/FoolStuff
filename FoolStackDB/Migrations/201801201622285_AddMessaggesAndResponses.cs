namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessaggesAndResponses : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RispostaMessaggios",
                c => new
                    {
                        Risposta_Id = c.Int(nullable: false),
                        Messaggio_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Risposta_Id, t.Messaggio_Id })
                .ForeignKey("dbo.Rispostas", t => t.Risposta_Id, cascadeDelete: false)
                .ForeignKey("dbo.Messaggios", t => t.Messaggio_Id, cascadeDelete: false)
                .Index(t => t.Risposta_Id)
                .Index(t => t.Messaggio_Id);
            
            CreateTable(
                "dbo.UserRispostas",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Risposta_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Risposta_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .ForeignKey("dbo.Rispostas", t => t.Risposta_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Risposta_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRispostas", "Risposta_Id", "dbo.Rispostas");
            DropForeignKey("dbo.UserRispostas", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Messaggios", "Submitter_Id", "dbo.Users");
            DropForeignKey("dbo.RispostaMessaggios", "Messaggio_Id", "dbo.Messaggios");
            DropForeignKey("dbo.RispostaMessaggios", "Risposta_Id", "dbo.Rispostas");
            DropIndex("dbo.UserRispostas", new[] { "Risposta_Id" });
            DropIndex("dbo.UserRispostas", new[] { "User_Id" });
            DropIndex("dbo.RispostaMessaggios", new[] { "Messaggio_Id" });
            DropIndex("dbo.RispostaMessaggios", new[] { "Risposta_Id" });
            DropIndex("dbo.Messaggios", new[] { "Submitter_Id" });
            DropTable("dbo.UserRispostas");
            DropTable("dbo.RispostaMessaggios");
            DropTable("dbo.Rispostas");
            DropTable("dbo.Messaggios");
        }
    }
}
