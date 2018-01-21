namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersCapitoliRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCapitoloes",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Capitolo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Capitolo_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Capitoloes", t => t.Capitolo_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Capitolo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCapitoloes", "Capitolo_Id", "dbo.Capitoloes");
            DropForeignKey("dbo.UserCapitoloes", "User_Id", "dbo.Users");
            DropIndex("dbo.UserCapitoloes", new[] { "Capitolo_Id" });
            DropIndex("dbo.UserCapitoloes", new[] { "User_Id" });
            DropTable("dbo.UserCapitoloes");
        }
    }
}
