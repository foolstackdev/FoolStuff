using FoolStackDB.Core.Domain;
using FoolStaff;
using FoolStuff.Dto;
using FoolStuff.Helpers;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThenInclude;

namespace FoolStuff.Controllers
{
    [RoutePrefix("api/formazione")]
    public class FormazioneController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getallcorsi")]
        public HttpResponseMessage GetAllCorsi()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //get user =>  User.Identity.GetUserId()
                    var entity = unitOfWork.Corsi.GetAllIncluding().Include(u => u.Utenti).Include(c => c.Capitoli.Select(f => f.ProgressiFormazione)).ToList();
                    //var entity = unitOfWork.Capitoli.GetAllIncluding().Include(c => c.Corso).Include(c => c.ProgressiFormazione).Include(t => t.Tags);
                    log.Debug("getallcorsi - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getallcorsi - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getcorsi/{id}")]
        public HttpResponseMessage Getcorsi(string id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entity = unitOfWork.Users.Search(u => u.Id == id)
                        .Include(e => e.Corsi.Select(c => c.Capitoli.Select(f => f.ProgressiFormazione.Select(u => u.Utente))))
                        .Include(e => e.Corsi.Select(c => c.Utenti.Select(f => f.ProgressiFormazione)))
                        .Include(e => e.ProgressiFormazione).FirstOrDefault();

                    log.Debug("getcorsi - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getcorsi - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("addcorso")]
        public HttpResponseMessage AddCorso([FromBody] Corso corso)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    unitOfWork.Corsi.Add(corso);
                    unitOfWork.Complete();
                    log.Debug("addcorso - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addcorso - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("addcapitolo")]
        public HttpResponseMessage AddCapitolo([FromBody] Corso corso)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entity = unitOfWork.Corsi.Search(c => c.Id == corso.Id).Include(c => c.Capitoli).FirstOrDefault();
                    entity.Capitoli.Add(corso.Capitoli.FirstOrDefault());
                    unitOfWork.Complete();
                    log.Debug("addcapitolo - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addcapitolo - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("addtag")]
        public HttpResponseMessage AddTag([FromBody] Capitolo capitolo)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entity = unitOfWork.Capitoli.Get(capitolo.Id);
                    if (entity.Tags.Contains(capitolo.Tags.FirstOrDefault()))
                    {
                        entity.Tags.Add(capitolo.Tags.FirstOrDefault());
                        unitOfWork.Complete();
                    }
                    log.Debug("addtag - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addtag - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("addusertocourse")]
        public HttpResponseMessage AddUsertoCourse([FromBody] FormazioneUserCorso formazioneUserCorso)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var corso = unitOfWork.Corsi.Search(u => u.Id == formazioneUserCorso.corso.Id).Include(u => u.Utenti).FirstOrDefault();
                    var utente = unitOfWork.Users.Search(u => u.Id == formazioneUserCorso.userId).FirstOrDefault();

                    if (!corso.Utenti.Contains(utente))
                    {
                        corso.Utenti.Add(utente);
                        unitOfWork.Complete();
                    }
                    else
                    {
                        string sMessage = "L'utente risulta gia iscritto al corso";
                        log.Debug("addusertocourse - errore " + sMessage);
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, sMessage);
                    }
                    log.Debug("addusertocourse - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addusertocourse - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("addprogressoformazione")]
        public HttpResponseMessage AddProgressoFormazione([FromBody] ProgressoFormazione progressoFormazione)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {

                    var entityCapitolo = unitOfWork.Capitoli.Get(progressoFormazione.Capitolo.Id);
                    var entityUser = unitOfWork.Users.Get(progressoFormazione.Utente.Id);
                    ProgressoFormazione oProgressoFOrmazione = new ProgressoFormazione();
                    oProgressoFOrmazione.Utente = entityUser;
                    oProgressoFOrmazione.DataCompletamento = UtilDate.CurrentTimeMillis();
                    oProgressoFOrmazione.Capitolo = entityCapitolo;
                    
                    unitOfWork.ProgressiFormazione.Add(oProgressoFOrmazione);
                    unitOfWork.Complete();

                    
                    log.Debug("addprogressoformazione - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addprogressoformazione - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
