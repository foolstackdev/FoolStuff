namespace FoolStackDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescrizioneToCorso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Corsoes", "Descrizione", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Corsoes", "Descrizione");
        }
    }
}
