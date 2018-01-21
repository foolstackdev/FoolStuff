using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Core.Domain
{
    public class Corso
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Risorsa { get; set; }
        public string Descrizione { get; set; }
        public IList<Capitolo> Capitoli { get; set; }
        public IList<User> Utenti { get; set; }
    }
}
