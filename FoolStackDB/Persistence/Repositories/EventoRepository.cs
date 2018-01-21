using FoolStaff.Core.Domain;
using FoolStaff.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FoolStackDB.Core.Domain;
using FoolStackDB.Core.Repositories;

namespace FoolStaff.Persistence.Repositories
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(FoolStaffContext context) : base(context)
        {
        }
        public FoolStaffContext FoolStaffContext
        {
            get { return Context as FoolStaffContext; }
        }
    }
}
