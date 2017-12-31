using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace FoolStuff.Helpers
{

    public class UtilIO
    {
        protected readonly string WORKING_DIRECTORY = ConfigurationManager.AppSettings["WorkingDirectory"];
        public string userId { get; set; }

        public UtilIO()
        {
            Directory.CreateDirectory(WORKING_DIRECTORY);
        }
        public void createUserDirectory()
        {
            try
            {
                string oUserDirectory = WORKING_DIRECTORY + Path.DirectorySeparatorChar + userId;
                Directory.CreateDirectory(oUserDirectory);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}