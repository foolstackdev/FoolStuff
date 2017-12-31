using FoolStaff;
using FoolStuff.Dto;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using FoolStuff.Helpers;
using FoolStuff.Manager;

namespace FoolStuff.Controllers
{
    [Authorize(Roles = "SuperAdmin, FoolStackUser")]
    [RoutePrefix("api/upload")]
    public class UploadController : ApiController
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private readonly string WORKING_DIRECTORY = ConfigurationManager.AppSettings["WorkingDirectory"];
        //private readonly string AVATAR_PATH = ConfigurationManager.AppSettings["AvatarPath"];

        ////Di seguito vanno inseriti i controlli di generazione di tutte le cartelle sopra descritte -> WORKING_DIRECTORY + SUBDIRECTORY
        //public UploadController()
        //{
        //    Directory.CreateDirectory(WORKING_DIRECTORY + AVATAR_PATH);
        //}


        [HttpPost]
        [Route("addavatar")]
        public HttpResponseMessage addAvatar(AvatarImages[] avatar)
        {
            IEnumerable<string> userIdValue;
            var userId = string.Empty;
            try
            {
                if (Request.Headers.TryGetValues("userid", out userIdValue))
                {
                    userId = userIdValue.FirstOrDefault();
                }
                else
                {
                    string sMessage = "No user in the headers";
                    log.Error(sMessage);
                    throw new Exception(sMessage);
                }

                Avatar oAvatar = new Avatar(userId);
                oAvatar.createAvatarDirectory();
                foreach (AvatarImages av in avatar)
                {
                    oAvatar.createImagesDirectory(av);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.Error("insertNewTask - errore nell'inserimento del task ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
