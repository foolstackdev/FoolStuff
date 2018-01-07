using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Dto
{
    public class FileLog
    {
        public string fileName { get; set; }
        public string content { get; set; }
        public long dateLastUpdate { get; set; }
        
    }
}