using FoolStackDB.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class FormazioneInsertTag
    {
        public string idCapitolo { get; set; }
        public Tag tag { get; set; }
    }
}