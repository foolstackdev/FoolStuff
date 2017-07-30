using FoolStaffDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoolStuff.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {

        [HttpGet]
        [Route("isAlive")]
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

                    var entity = entities.Task.ToList().Where(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita);
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
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

                    var entity = entities.Task.ToList().Where(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita);
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }




    }
}
