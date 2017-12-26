using FoolStaff.Core.Domain;
using FoolStaff.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FoolStaff.Persistence.Repositories
{
    public class EffortRepository : Repository<Effort>, IEffortRepository
    {
        public EffortRepository(FoolStaffContext context) : base(context)
        {
        }
    }
}
