using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Core.Domain
{
    public class Messaggio
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Testo { get; set; }
        public long DataMessaggio { get; set; }

        public User Submitter { get; set; }
        public IList<Risposta> Risposte { get; set; }

        //Di seguito relazioni con tabelle di decodifica contestuali (Es: Capitoli Formazione, Tesoreria, altri messaggi ad hoc )
        public IList<Capitolo> Capitoli { get; set; }
    
    }
}
