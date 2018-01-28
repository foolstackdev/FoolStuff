using FoolStackDB.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Persistence.EntityConfigurations
{
    class MessaggioConfiguration: EntityTypeConfiguration<Messaggio>
    {
        public MessaggioConfiguration()
        {
            HasMany(m => m.Risposte)
                .WithRequired(r => r.Messaggio)
                .WillCascadeOnDelete(false);

        }
    }
}
