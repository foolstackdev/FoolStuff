using FoolStackDB.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Persistence.EntityConfigurations
{
    class CapitoloConfiguration : EntityTypeConfiguration<Capitolo>
    {
        public CapitoloConfiguration()
        {
            HasRequired(c => c.Corso)
                .WithMany(e => e.Capitoli);

            HasMany(c => c.Tags)
                .WithMany(t => t.Capitoli);

            HasMany(m => m.Messaggi)
                .WithMany(c => c.Capitoli);

            HasMany(p => p.ProgressiFormazione)
                .WithRequired(c => c.Capitolo)
                .WillCascadeOnDelete(false);
        }
    }
}
