using FoolStackDB.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStaff.Core.Domain
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<Tesoreria> Tesoreria { get; set; }
        public IList<Effort> Efforts { get; set; }
        public IList<Evento> Prenotazioni { get; set; }
        public IList<Evento> Presenze { get; set; }

        //Gestione Messaggi e Risposte
        public IList<Messaggio> Messaggi { get; set; }
        public IList<Risposta> Risposte { get; set; }
        //Getsione formazione
        //public IList<Capitolo> Capitoli { get; set; }
        public IList<ProgressoFormazione> ProgressiFormazione { get; set; }

        public IList<Corso> Corsi { get; set; }

    }
}
