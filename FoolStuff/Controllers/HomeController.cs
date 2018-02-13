using FoolStaff;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoolStuff.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getallinfo")]
        public HttpResponseMessage GetAllCorsi()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //var entity = unitOfWork.Messaggi.GetAllIncluding().Include(r => r.Risposte).OrderBy(f => f.Risposte.Select(d => d.DataRisposta)).Take(5).ToList();
                    var entity = unitOfWork.Messaggi.GetAllIncluding().Include(r => r.Risposte).OrderByDescending(d => d.DataMessaggio).Take(5).ToList();

                    //get user =>  User.Identity.GetUserId()
                    //var entity = unitOfWork.Corsi.GetAllIncluding().Include(u => u.Utenti).Include(c => c.Capitoli.Select(f => f.ProgressiFormazione)).Include(c => c.Capitoli.Select(m => m.Messaggi)).ToList();
                    //var entity = unitOfWork.Corsi.GetAllIncluding().Include(u => u.Utenti).Include(c => c.Capitoli.Select(f => f.ProgressiFormazione)).ToList();
                    //var entity = unitOfWork.Corsi.GetAllIncluding().Include(c => c.Capitoli).Take(2).ToList();
                    log.Debug("getallinfo - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getallinfo - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
