using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStaff.Core.Domain
{
    public class Effort
    {
        public int Id { get; set; }
        public long DataCreazione { get; set; }
        public long DataChiusura { get; set; }
        public string Stato { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public int Priorita { get; set; }

        public IList<User> Users { get; set; }
    }
}
