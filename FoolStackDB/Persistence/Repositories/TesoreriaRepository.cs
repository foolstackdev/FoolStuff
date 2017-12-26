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
    public class TesoreriaRepository : Repository<Tesoreria>, ITesoreriaRepository
    {
        public TesoreriaRepository(FoolStaffContext context) : base(context)
        {
        }
        public FoolStaffContext FoolStaffContext
        {
            get { return Context as FoolStaffContext; }
        }
    }
}
