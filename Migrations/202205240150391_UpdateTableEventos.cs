namespace AthonEventos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableEventos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evento", "Imagem", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evento", "Imagem");
        }
    }
}
