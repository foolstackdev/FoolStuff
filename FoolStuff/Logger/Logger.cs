using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;


namespace FoolStuff.Logger
{
    public class Logger
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);

        }

        internal static ILog GetLogger(Type declaringType)
        {
            throw new NotImplementedException();
        }
    }
}