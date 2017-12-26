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
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tesoreria> Tesoreria{ get; set; }
        public virtual DbSet<Effort> Efforts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new TesoreriaConfiguration());
            modelBuilder.Configurations.Add(new EffortConfiguration());
        }
    }
}
