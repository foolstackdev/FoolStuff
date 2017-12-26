using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class TesoreriaInsertVersamento
    {
        public long dataOperazione { get; set; }
        public string operazione { get; set; }
        public decimal quota { get; set; }
        public string note { get; set; }
        public List<User> users { get; set; }
    }
}