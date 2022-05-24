namespace AthonEventos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificado",
                c => new
                    {
                        CertificadoID = c.Int(nullable: false, identity: true),
                        UsuarioID = c.Int(nullable: false),
                        PalestraID = c.Int(nullable: false),
                        CertificadoDt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CertificadoID)
                .ForeignKey("dbo.Palestra", t => t.PalestraID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID)
                .Index(t => t.PalestraID);
            
            CreateTable(
                "dbo.Palestra",
                c => new
                    {
                        PalestraID = c.Int(nullable: false, identity: true),
                        PalestraName = c.String(nullable: false, maxLength: 50),
                        PalestraTema = c.String(nullable: false, maxLength: 50),
                        PalestraDescription = c.String(nullable: false, maxLength: 250),
                        PalestraUrl = c.String(nullable: false, maxLength: 250),
                        PalestraDt = c.DateTime(nullable: false),
                        PalestranteID = c.Int(nullable: false),
                        EventoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PalestraID)
                .ForeignKey("dbo.Evento", t => t.EventoID)
                .ForeignKey("dbo.Palestrante", t => t.PalestranteID)
                .Index(t => t.PalestranteID)
                .Index(t => t.EventoID);
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        EventoID = c.Int(nullable: false, identity: true),
                        EventoName = c.String(nullable: false, maxLength: 50),
                        EventoDescricao = c.String(nullable: false, maxLength: 450),
                        EventoDtInicio = c.DateTime(nullable: false),
                        EventoDtFim = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventoID);
            
            CreateTable(
                "dbo.Palestrante",
                c => new
                    {
                        PalestranteID = c.Int(nullable: false, identity: true),
                        PalestranteDescrição = c.String(maxLength: 450),
                        UsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PalestranteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioID = c.Int(nullable: false, identity: true),
                        UsuarioName = c.String(nullable: false, maxLength: 50),
                        UsuarioSobrenome = c.String(nullable: false, maxLength: 50),
                        UsuarioEmail = c.String(nullable: false, maxLength: 50),
                        UsuarioDt = c.DateTime(nullable: false),
                        UsuarioPassword = c.String(nullable: false, maxLength: 10),
                        UsuarioEhAluno = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Certificado", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Certificado", "PalestraID", "dbo.Palestra");
            DropForeignKey("dbo.Palestra", "PalestranteID", "dbo.Palestrante");
            DropForeignKey("dbo.Palestrante", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Palestra", "EventoID", "dbo.Evento");
            DropIndex("dbo.Palestrante", new[] { "UsuarioID" });
            DropIndex("dbo.Palestra", new[] { "EventoID" });
            DropIndex("dbo.Palestra", new[] { "PalestranteID" });
            DropIndex("dbo.Certificado", new[] { "PalestraID" });
            DropIndex("dbo.Certificado", new[] { "UsuarioID" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Palestrante");
            DropTable("dbo.Evento");
            DropTable("dbo.Palestra");
            DropTable("dbo.Certificado");
        }
    }
}
