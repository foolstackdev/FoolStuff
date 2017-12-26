using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStaff.Core.Domain
{
    public class Tesoreria
    {
        public int Id { get; set; }
        public long DataOperazione { get; set; }
        public string Operazione { get; set; }
        public decimal Quota { get; set; }
        public string Note { get; set; }
        public IList<User> user { get; set; }
    }
}
