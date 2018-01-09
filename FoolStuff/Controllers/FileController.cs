using FoolStuff.Dto;
using FoolStuff.Helpers;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoolStuff.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("getlogfilelist")]
        public HttpResponseMessage getLogFileList()
        {
            try
            {
                List<FileLog> fileEntries = new List<FileLog>();
                log4net.Appender.IAppender[] logAppenders = log.Logger.Repository.GetAppenders();

                foreach(log4net.Appender.IAppender s in logAppenders)
                {
                    string filePath = ((log4net.Appender.FileAppender)s).File;
                    DateTime oTime = System.IO.File.GetLastWriteTime(filePath);

                    FileLog oFileLog = new FileLog();
                    oFileLog.fileName = Path.GetFileName(filePath);
                    oFileLog.dateLastUpdate = UtilDate.CurrentTimeMillis(oTime);

                    fileEntries.Add(oFileLog);
                }
                log.Debug("getlogfilelist - metodo eseguito con successo");
                return Request.CreateResponse(HttpStatusCode.OK,fileEntries);

            }
            catch (Exception ex)
            {
                log.Error("getlogfilelist - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("getlogfile/{sfilename}")]
        public HttpResponseMessage getLogFile(string sfilename)
        {
            try
            {
                List<FileLog> fileEntries = new List<FileLog>();
                log4net.Appender.IAppender[] logAppenders = log.Logger.Repository.GetAppenders();
                FileLog oFileLog = new FileLog();
                foreach (log4net.Appender.IAppender s in logAppenders)
                {
                    string filePath = ((log4net.Appender.FileAppender)s).File;
                    string fileName = Path.GetFileName(filePath);

                    if(sfilename != fileName)
                    {
                        continue;
                    }

                    DateTime oTime = System.IO.File.GetLastWriteTime(filePath);
                    oFileLog.fileName = Path.GetFileName(filePath);
                    oFileLog.dateLastUpdate = UtilDate.CurrentTimeMillis(oTime);
                    string content = "";

                    using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                content = reader.ReadToEnd();
                            }
                        }
                    }
                    oFileLog.content = content;
                    break;
                }
                log.Debug("getLogFile - metodo eseguito con successo");
                return Request.CreateResponse(HttpStatusCode.OK, oFileLog);

            }
            catch (Exception ex)
            {
                log.Error("getLogFile - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
    }
}
