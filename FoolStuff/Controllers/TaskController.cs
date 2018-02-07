using FoolStaff;
using FoolStaff.Core.Domain;
using FoolStuff.Helpers;
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
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getalltask")]
        public HttpResponseMessage getAllTask()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //entities.Configuration.ProxyCreationEnabled = false;
                    var entity = unitOfWork.Efforts.Search(t => t.Stato == "OPEN").Include(c => c.Users).OrderByDescending(t => t.Priorita).ToList();
                    log.Debug("getAllTask - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getAllTask - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("insertnewtask")]
        public HttpResponseMessage insertNewTask([FromBody]Effort effort)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    Effort oEffort = new Effort();
                    oEffort.Titolo = effort.Titolo;
                    oEffort.Descrizione = effort.Descrizione;
                    oEffort.Priorita = effort.Priorita;
                    oEffort.DataCreazione = UtilDate.CurrentTimeMillis();
                    oEffort.Stato = "OPEN";

                    unitOfWork.Efforts.Add(oEffort);
                    unitOfWork.Complete();

                    var entity = unitOfWork.Efforts.Find(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita).ToList();
                    log.Debug("insertNewTask - task inserito correttamente");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("insertNewTask - errore nell'inserimento del task ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("addusertotask/{userid}")]
        public HttpResponseMessage addUserToTask(string userId, [FromBody]int idEffort)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    User user = unitOfWork.Users.Search(u => u.Id == userId).Include(e => e.Efforts).FirstOrDefault();
                    Effort entityEffort = unitOfWork.Efforts.SingleOrDefault(t => t.Id == idEffort);
                    user.Efforts.Add(entityEffort);
                    unitOfWork.Complete();

                    var entity = unitOfWork.Efforts.Search(t => t.Stato == "OPEN").Include(c => c.Users).OrderByDescending(t => t.Priorita).ToList();
                    log.Debug("addUserToTask - utente id [" + unitOfWork.Users.SingleOrDefault(u => u.Id == userId).Id + "] correttamente associato al task");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("addUserToTask - errore nell'associazione dell'utente al task ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("giveuptask/{userid}")]
        public HttpResponseMessage giveUpTask(string userId, [FromBody]int idEffort)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {

                    User entityUser = unitOfWork.Users.Search(u => u.Id == userId).SingleOrDefault();
                    Effort entityTask = unitOfWork.Efforts.Search(t => t.Id == idEffort).Include(c => c.Users).SingleOrDefault();

                    if (entityTask.Users.Contains(entityUser))
                    {
                        entityTask.Users.Remove(entityUser);
                        unitOfWork.Complete();
                    }

                    var entity = unitOfWork.Efforts.Find(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita).ToList();
                    log.Debug("giveUpTask - rinuncia al task da parte dell'utente id [" + entityUser.Id + "] avvenuta con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("giveUpTask - errore nella rinuncia al task ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("closetask")]
        public HttpResponseMessage closeTask([FromBody]int idEffort)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entityTask = unitOfWork.Efforts.SingleOrDefault(t => t.Id == idEffort);

                    if (entityTask != null)
                    {
                        entityTask.DataChiusura = UtilDate.CurrentTimeMillis();
                        entityTask.Stato = "CLOSED";
                        unitOfWork.Complete();
                    }
                    var entity = unitOfWork.Efforts.Find(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita).ToList();
                    log.Debug("closeTask - Task id [" + entityTask.Id + "] chiuso correttamente");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("closeTask - errore nella chiusura al task ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("reopentask")]
        public HttpResponseMessage reopenTask([FromBody]int idEffort)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entityTask = unitOfWork.Efforts.SingleOrDefault(t => t.Id == idEffort);

                    if (entityTask != null)
                    {
                        //entityTask.DataChiusura = UtilDate.CurrentTimeMillis();
                        entityTask.Stato = "OPEN";
                        unitOfWork.Complete();
                    }
                    var entity = unitOfWork.Efforts.Find(t => t.Stato == "CLOSED").OrderByDescending(t => t.Priorita).ToList();
                    log.Debug("reopenTask - Task id [" + entityTask.Id + "] riaperto correttamente");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("reopenTask - errore nella riaperture del task ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getclosedtask")]
        public IHttpActionResult getClosedTask()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entityTask = unitOfWork.Efforts.Search(t => t.Stato == "CLOSED").Include(u => u.Users).OrderByDescending(t => t.DataChiusura).ToList();
                    log.Debug("getClosedTask - Lista Task chiusi correttamente recuperata");
                    return Ok(entityTask);
                }
            }
            catch (Exception ex)
            {
                log.Error("getClosedTask - errore nella visualizzazione dei task chiusi ", ex);
                return InternalServerError(ex);
            }
        }
    }
}
