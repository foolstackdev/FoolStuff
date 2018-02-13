using FoolStackDB.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class FormazioneGetCorsi
    {
        public string Titolo { get; set; }
        public IList<Capitolo> capitoli { get; set; }
        public FormazioneGetCorsi(Corso oCorso)
        {
            this.Titolo = oCorso.Titolo;
            this.capitoli = oCorso.Capitoli;
        }

    }
}