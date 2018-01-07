using FoolStaff.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class EventsPresence
    {
        public int eventId { get; set; }
        public List<User> users { get; set; }
    }
}