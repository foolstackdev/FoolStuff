using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoolStuff.Helpers
{
    public class UtilDate
    {
        private static readonly DateTime Jan1st1970 = new DateTime
      (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
        public static long CurrentTimeMillis(DateTime oTime)
        {
            return (long)(oTime - Jan1st1970).TotalMilliseconds;
        }
    }
}