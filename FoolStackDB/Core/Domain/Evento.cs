using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Core.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public long DataEvento { get; set; }
        public string Titolo { get; set; }
        public string Note { get; set; }

        public IList<User> Prenotazioni { get; set; }
        public IList<User> Presenze { get; set; }
    }
}
