namespace FoolStaff.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventoDBEntity : DbMigration
    {
        public override void Up()
        {
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
                "dbo.UserEventoes",
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
            DropForeignKey("dbo.UserEventoes", "Evento_Id", "dbo.Eventoes");
            DropForeignKey("dbo.UserEventoes", "User_Id", "dbo.Users");
            DropIndex("dbo.UserEventoes", new[] { "Evento_Id" });
            DropIndex("dbo.UserEventoes", new[] { "User_Id" });
            DropTable("dbo.UserEventoes");
            DropTable("dbo.Eventoes");
        }
    }
}
