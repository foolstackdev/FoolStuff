using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Core.Domain
{
    public class ProgressoFormazione
    {
        public int Id { get; set; }
        public long DataCompletamento { get; set; }
        public User Utente { get; set; }
        public Capitolo Capitolo { get; set; }
    }
}
