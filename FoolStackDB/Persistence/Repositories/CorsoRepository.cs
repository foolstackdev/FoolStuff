using FoolStackDB.Core.Domain;
using FoolStackDB.Core.Repositories;
using FoolStaff;
using FoolStaff.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Persistence.Repositories
{
    public class CorsoRepository : Repository<Corso>, ICorsoRepository
    {
        public CorsoRepository(FoolStaffContext context) : base(context)
        {
        }
        public FoolStaffContext FoolStaffContext
        {
            get { return Context as FoolStaffContext; }
        }
    }
}
