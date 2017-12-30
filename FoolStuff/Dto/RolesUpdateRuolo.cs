using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class RolesUpdateRuolo
    {
        public string userId { get; set; }
        public string roleId { get; set; }
        public string role { get; set; }
        public string oldrole { get; set; }
    }
}