namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProgressoFormazione : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCapitoloes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserCapitoloes", "Capitolo_Id", "dbo.Capitoloes");
            DropIndex("dbo.UserCapitoloes", new[] { "User_Id" });
            DropIndex("dbo.UserCapitoloes", new[] { "Capitolo_Id" });
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
            
            DropTable("dbo.UserCapitoloes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserCapitoloes",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Capitolo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Capitolo_Id });
            
            DropForeignKey("dbo.ProgressoFormaziones", "Capitolo_Id", "dbo.Capitoloes");
            DropForeignKey("dbo.ProgressoFormaziones", "Utente_Id", "dbo.Users");
            DropIndex("dbo.ProgressoFormaziones", new[] { "Capitolo_Id" });
            DropIndex("dbo.ProgressoFormaziones", new[] { "Utente_Id" });
            DropTable("dbo.ProgressoFormaziones");
            CreateIndex("dbo.UserCapitoloes", "Capitolo_Id");
            CreateIndex("dbo.UserCapitoloes", "User_Id");
            AddForeignKey("dbo.UserCapitoloes", "Capitolo_Id", "dbo.Capitoloes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserCapitoloes", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
