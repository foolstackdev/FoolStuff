using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Core.Domain
{
    public class Capitolo
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public int NumeroCapitolo { get; set; }
        public Corso Corso { get; set; }
        public IList<Messaggio> Messaggi { get; set; }
        public IList<Tag> Tags { get; set; }
        //public IList<User> Utenti { get; set; }
        public IList<ProgressoFormazione> ProgressiFormazione { get; set; }
    }
}
