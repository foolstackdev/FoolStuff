namespace FoolStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateDB : DbMigration
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
                "dbo.Tesorerias",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DataOperazione = c.Long(nullable: false),
                    Operazione = c.String(),
                    Quota = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Note = c.String(),
                    user_Id = c.String(maxLength: 128, nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.user_Id)
                .Index(t => t.user_Id);

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

        }

        public override void Down()
        {
            DropForeignKey("dbo.Tesorerias", "user_Id", "dbo.Users");
            DropForeignKey("dbo.UserEfforts", "Effort_Id", "dbo.Efforts");
            DropForeignKey("dbo.UserEfforts", "User_Id", "dbo.Users");
            DropIndex("dbo.UserEfforts", new[] { "Effort_Id" });
            DropIndex("dbo.UserEfforts", new[] { "User_Id" });
            DropIndex("dbo.Tesorerias", new[] { "user_Id" });
            DropTable("dbo.UserEfforts");
            DropTable("dbo.Tesorerias");
            DropTable("dbo.Users");
            DropTable("dbo.Efforts");
        }
    }
}
