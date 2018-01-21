using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Core.Domain
{
    public class Tag
    {
        public int Id { get; set; }
        public string Testo { get; set; }
        public IList<Capitolo> Capitoli {get;set;}
    }
}
