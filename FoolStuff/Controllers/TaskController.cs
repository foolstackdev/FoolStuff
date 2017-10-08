using FoolStaffDataAccess;
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

                    var entity = entities.Task.Where(t => t.Stato == "OPEN").Include(t => t.UserInfo).OrderByDescending(t => t.Priorita).ToList();
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

        [HttpPost]
        [Route("addusertotask/{userid}")]
        public HttpResponseMessage insertNewTask(string userId, [FromBody]int idTask)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {

                    var entityUser = entities.UserInfo.FirstOrDefault(u => u.Id == userId);
                    var entityTask = entities.Task.Include(t => t.UserInfo).FirstOrDefault(t => t.Id == idTask);

                    if (!entityTask.UserInfo.Contains(entityUser))
                    {
                        entityTask.UserInfo.Add(entityUser);
                        entities.SaveChanges();
                    }

                    //var entity = entities.Task.ToList().Where(t => t.Stato == "OPEN").OrderByDescending(t => t.Priorita);
                    var entity = entities.Task.Where(t => t.Stato == "OPEN").Include(t => t.UserInfo).OrderByDescending(t => t.Priorita).ToList(); 
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
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
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
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
                        entityTask.Stato = "CLOSED";
                        entities.SaveChanges();
                    }
                    var entity = entities.Task.Where(t => t.Stato == "OPEN").Include(t => t.UserInfo).OrderByDescending(t => t.Priorita).ToList(); 
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("getclosedtask")]
        public HttpResponseMessage getClosedTask()
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    var entityTask = entities.Task.Include(t => t.UserInfo).ToList();

                    var entity = entities.Task.Where(t => t.Stato == "CLOSED").Include(t => t.UserInfo).OrderByDescending(t => t.Priorita).ToList(); 
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
