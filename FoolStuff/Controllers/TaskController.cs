using FoolStaffDataAccess;
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
    [Authorize(Roles = "SuperAdmin, FoolStackUser")]
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [HttpGet]
        [Route("isalive")]
        public HttpResponseMessage isAlive()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "I'm Alive, Hello!");
        }

        [HttpGet]
        [Route("getalltask")]
        public HttpResponseMessage getAllTask()
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    //entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.Task.Where(t => t.Stato == "OPEN").Include(f => f.UserInfo).OrderByDescending(t => t.Priorita).ToList();
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

        [HttpPost]
        [Route("insertnewtask")]
        public HttpResponseMessage insertNewTask([FromBody]Task task)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    Task oTask = new Task();
                    oTask.Titolo = task.Titolo;
                    oTask.Descrizione = task.Descrizione;
                    oTask.Priorita = task.Priorita;
                    oTask.DataCreazione = DateTime.Now;
                    oTask.Stato = "OPEN";

                    entities.Task.Add(oTask);
                    entities.SaveChanges();

                    var entity = entities.Task.Where(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita).ToList();
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

        [HttpPost]
        [Route("addusertotask/{userid}")]
        public HttpResponseMessage insertNewTask(string userId, [FromBody]int idTask)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {

                    

                    var entityUser = entities.UserInfo.FirstOrDefault(u => u.Id == userId);
                    entities.Task.FirstOrDefault(t => t.Id == idTask).UserInfo.Add(entityUser);
                    entities.SaveChanges();
                    //var entityTask = entities.Task.Include(t => t.UserInfo).FirstOrDefault(t => t.Id == idTask);

                    //if (!entityTask.UserInfo.Contains(entityUser))
                    //{
                    //    entityTask.UserInfo.Add(entityUser);
                    //    entities.SaveChanges();
                    //}

                    //var entity = entities.Task.ToList().Where(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita);
                    var entity = entities.Task.Where(t => t.Stato == "OPEN").Include(t => t.UserInfo).OrderByDescending(t => t.Priorita).ToList();
                    log.Debug("addUserToTask - utente id [" + entityUser.Id + "] correttamente associato al task");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("addUserToTask - errore nell'associazione dell'utente al task ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("giveuptask/{userid}")]
        public HttpResponseMessage giveUpTask(string userId, [FromBody]int idTask)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {

                    var entityUser = entities.UserInfo.FirstOrDefault(u => u.Id == userId);
                    var entityTask = entities.Task.Include(t => t.UserInfo).FirstOrDefault(t => t.Id == idTask);

                    if (entityTask.UserInfo.Contains(entityUser))
                    {
                        entityTask.UserInfo.Remove(entityUser);
                        entities.SaveChanges();
                    }

                    //var entity = entities.Task.ToList().Where(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita);
                    var entity = entities.Task.Where(t => t.Stato == "OPEN").Include(t => t.UserInfo).OrderByDescending(t => t.Priorita).ToList();
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

        [HttpPost]
        [Route("closetask")]
        public HttpResponseMessage closeTask([FromBody]int idTask)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    var entityTask = entities.Task.FirstOrDefault(t => t.Id == idTask);

                    if (entityTask != null)
                    {
                        entityTask.DataChiusura = DateTime.Today; 
                        entityTask.Stato = "CLOSED";
                        entities.SaveChanges();
                    }
                    var entity = entities.Task.Where(t => t.Stato == "OPEN").Include(t => t.UserInfo).OrderByDescending(t => t.Priorita).ToList();
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

        [HttpGet]
        [Route("getclosedtask")]
        public IHttpActionResult getClosedTask()
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    //entities.Configuration.LazyLoadingEnabled = false;
                    //entities.Configuration.ProxyCreationEnabled = false;

                    //var example = from user in entities.UserInfo
                    //              from tasks in user.Task
                    //              select new
                    //              {
                    //                  user = user,
                    //                  tasks = tasks
                    //              };

                    var entityTask = entities.Task.Where(t => t.Stato == "CLOSED").Include(t => t.UserInfo).OrderByDescending(t => t.DataChiusura).ToList();
                    log.Debug("getClosedTask - Lista Task chiusi correttamente recuperata");
                    //var entity = entities.Task.Where(t => t.Stato == "CLOSED").Include(t => t.UserInfo).OrderByDescending(t => t.DataChiusura).ToList(); 
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
