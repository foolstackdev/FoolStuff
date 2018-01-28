using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Core.Domain
{
    public class Risposta
    {
        public int Id { get; set; }
        public long DataRisposta { get; set; }
        public string Testo { get; set; }

        public Messaggio Messaggio { get; set; }
        public User Utente { get; set; }
    }
}
