using FoolStackDB.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Persistence.EntityConfigurations
{
    class EventoConfiguration : EntityTypeConfiguration<Evento>
    {
        public EventoConfiguration()
        {
            Property(c => c.DataEvento)
             .IsRequired();

            Property(c => c.Titolo)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
