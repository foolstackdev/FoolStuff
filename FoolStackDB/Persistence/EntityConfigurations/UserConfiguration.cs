using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStaff.Persistence.EntityConfigurations
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
            .HasMaxLength(128)
            .IsRequired();

            HasMany(e => e.Efforts)
                .WithMany(u => u.Users);

            HasMany(e => e.Prenotazioni)
                .WithMany(u => u.Prenotazioni)
                .Map(c => {
                    c.ToTable("Prenotazioni");
                });

            HasMany(e => e.Presenze)
                .WithMany(u => u.Presenze)
                .Map(c => {
                    c.ToTable("Presenze");
                });

            HasMany(t => t.Tesoreria)
                .WithMany(u => u.user);

            HasMany(m => m.Messaggi)
                .WithRequired(u => u.Submitter)
                .WillCascadeOnDelete(false);

            HasMany(r => r.Risposte)
                .WithRequired(u => u.Utente)
                .WillCascadeOnDelete(false);

            HasMany(p => p.ProgressiFormazione)
                .WithRequired(u => u.Utente)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Corsi)
               .WithMany(u => u.Utenti)
               .Map(c =>
               {
                   c.ToTable("CorsiUtenti");
               });
        }
    }
}
