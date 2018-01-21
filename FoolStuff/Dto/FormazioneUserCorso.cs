using FoolStackDB.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class FormazioneUserCorso
    {
        public string userId { get; set; }
        public Corso corso { get; set; }
    }
}