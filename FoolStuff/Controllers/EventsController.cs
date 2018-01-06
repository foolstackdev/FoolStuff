using FoolStackDB.Core.Domain;
using FoolStaff;
using FoolStaff.Core.Domain;
using FoolStuff.Dto;
using FoolStuff.Helpers;
using FoolStuff.Models;
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
    [RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //[Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getalleventswithusers")]
        public HttpResponseMessage getAllEventsWithUsers()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entity = unitOfWork.Eventi.GetAllWithInclude(u => u.Prenotazioni).OrderByDescending(t => t.DataEvento).ToList();
                    log.Debug("getalleventswithusers - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getalleventswithusers - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("getnextevents")]
        public HttpResponseMessage getNextEvents()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    long dataOdierna = UtilDate.CurrentTimeMillis();
                    var entity = unitOfWork.Eventi.Search(e => e.DataEvento > dataOdierna).Include(u => u.Prenotazioni).OrderByDescending(t => t.DataEvento).ToList();
                    log.Debug("getnextevents - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getnextevents - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("getfirstnumeventswithusers/{num}")]
        public HttpResponseMessage getFirstNumEventsWithUsers(string num)
        {
            int numbersOfRows;
            try
            {
                numbersOfRows = Convert.ToInt16(num);
            }
            catch (Exception e)
            {
                numbersOfRows = 6;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    
                    var entity = unitOfWork.Eventi.GetAllIncluding().OrderByDescending(t => t.DataEvento).Take(numbersOfRows).Include(e => e.Prenotazioni).Include(e => e.Presenze).ToList();
                    log.Debug("getfirstnumeventswithusers - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getfirstnumeventswithusers - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("insertnewevent")]
        public HttpResponseMessage insertNewEvent([FromBody]Evento evento)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    unitOfWork.Eventi.Add(evento);
                    unitOfWork.Complete();
                    log.Debug("insertnewevent - evento inserito correttamente");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("insertNewTask - errore nell'inserimento del evento ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("addusertoevent")]
        public HttpResponseMessage addUsertoEvent([FromBody]EventsPrenotazione evento)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    User user = unitOfWork.Users.Search(u => u.Id == evento.userId).Include(e => e.Prenotazioni).FirstOrDefault();
                    Evento entityEvent = unitOfWork.Eventi.SingleOrDefault(t => t.Id == evento.eventId);
                    user.Prenotazioni.Add(entityEvent);
                    unitOfWork.Complete();
                    log.Debug("addusertoevent - evento inserito correttamente");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addusertoevent - errore nell'inserimento del evento ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
