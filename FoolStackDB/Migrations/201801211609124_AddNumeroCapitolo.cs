namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumeroCapitolo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Capitoloes", "NumeroCapitolo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Capitoloes", "NumeroCapitolo");
        }
    }
}
