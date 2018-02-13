using FoolStackDB.Core.Domain;
using FoolStackDB.Persistence.EntityConfigurations;
using FoolStaff.Core.Domain;
using FoolStaff.Persistence.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStaff
{
    public class FoolStaffContext : DbContext
    {
        public FoolStaffContext()
            : base("name=FooStaffContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tesoreria> Tesoreria{ get; set; }
        public virtual DbSet<Effort> Efforts { get; set; }
        public virtual DbSet<Evento> Eventi { get; set; }
        public virtual DbSet<Messaggio> Messaggi { get; set; }
        public virtual DbSet<Risposta> Risposte { get; set; }
        public virtual DbSet<Corso> Corsi { get; set; }
        public virtual DbSet<Capitolo> Capitoli { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<ProgressoFormazione> ProgressiFormazione { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new TesoreriaConfiguration());
            modelBuilder.Configurations.Add(new EffortConfiguration());
            modelBuilder.Configurations.Add(new EventoConfiguration());
            modelBuilder.Configurations.Add(new MessaggioConfiguration());
            modelBuilder.Configurations.Add(new RispostaConfiguration());
            modelBuilder.Configurations.Add(new CorsoConfiguration());
            modelBuilder.Configurations.Add(new CapitoloConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new ProgressoFormazioneConfiguration());

        }
    }
}
