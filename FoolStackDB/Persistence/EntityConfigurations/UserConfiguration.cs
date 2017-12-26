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


            HasMany(t => t.Tesoreria)
                .WithMany(u => u.user);
                //.WillCascadeOnDelete(false);
            
        }
    }
}
