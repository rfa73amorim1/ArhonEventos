using AthonEventos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AthonEventos.DAL
{
    public class AthonEventosContext : DbContext
    {
        public AthonEventosContext() : base("AthonEventosContext")
        {
        }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Palestra> Palestras { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }


    }
}