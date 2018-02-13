using FoolStackDB.Core.Domain;
using FoolStaff;
using FoolStaff.Core.Domain;
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
using Z.EntityFramework.Plus;

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
                    //Recupera l'utente corrente dalla sessione del servizio rest
                    //get user =>  User.Identity.GetUserId()

                    var entity = unitOfWork.Corsi.GetAllIncluding().Include(c => c.Capitoli).AsNoTracking().ToList();
                    //List<Corso> listcorsi = unitOfWork.Corsi.GetAll().ToList();
                    //foreach(Corso oCorso in listcorsi)
                    //{
                    //    oCorso.Capitoli = unitOfWork.Capitoli.Find(c => c.Corso.Id == oCorso.Id).ToList();
                    //}

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
        [HttpPost]
        [Route("getcorso")]
        public HttpResponseMessage GetCorso([FromBody] Corso corso)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //get user =>  User.Identity.GetUserId()
                    string _id = User.Identity.GetUserId();

                    var entity = unitOfWork.Corsi.Search(r => r.Id == corso.Id)
                        .Include(u => u.Utenti)
                        .Include(c => c.Capitoli.Select(f => f.ProgressiFormazione.Select(o => o.Utente)))
                        .Include(c => c.Capitoli.Select(m => m.Messaggi))
                        .AsNoTracking()
                        .FirstOrDefault();

                    log.Debug("getcorso - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getcorso - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("getprogressoformazionebyidcorso")]
        public HttpResponseMessage GetProgressoFormazioneByIdECorso([FromBody] FormazioneGetProgressoFormazione progressoformazione)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //var entity = unitOfWork.ProgressiFormazione.Find(e => e.Utente.Id == progressoformazione.idUtente && e.Capitolo.Corso.Id == progressoformazione.idCorso).ToList();

                    //var entity = unitOfWork.Users.Search(e => e.ProgressiFormazione.Any(g => g.Capitolo.Corso.Id == progressoformazione.idCorso))
                    //    .Include(p => p.ProgressiFormazione)
                    //    .Where(f => f.ProgressiFormazione.Any(g => g.Capitolo.Corso.Id == progressoformazione.idCorso))
                    //    .AsNoTracking().ToList();

                    List<User> entity = unitOfWork.Users.Search(e => e.Corsi.Any(g => g.Id == progressoformazione.idCorso)).ToList();
                    foreach(User u in entity)
                    {
                        u.ProgressiFormazione = unitOfWork.ProgressiFormazione.Find(e => e.Utente.Id == u.Id && e.Capitolo.Corso.Id == progressoformazione.idCorso).ToList();
                    }

                    log.Debug("getprogressoformazionebyidcorso - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getprogressoformazionebyidcorso - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //[Authorize(Roles = "SuperAdmin, FoolStackUser")]
        //[HttpGet]
        //[Route("getcorsi/{id}")]
        //public HttpResponseMessage Getcorsi(string id)
        //{
        //    try
        //    {
        //        using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
        //        {
        //            var entity = unitOfWork.Users.Search(u => u.Id == id)
        //                .Include(e => e.Corsi.Select(c => c.Capitoli.Select(f => f.ProgressiFormazione.Select(u => u.Utente))))
        //                .Include(e => e.Corsi.Select(c => c.Utenti.Select(f => f.ProgressiFormazione)))
        //                .Include(e => e.ProgressiFormazione).FirstOrDefault();

        //            log.Debug("getcorsi - metodo eseguito con successo");
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("getcorsi - errore nell'esecuzione ", ex);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
        //    }
        //}

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

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("addusermessageforcapitolo")]
        public HttpResponseMessage AddUserMessageForCapitolo([FromBody] Capitolo capitolo)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    Capitolo oCapitolo = unitOfWork.Capitoli.Search(u => u.Id == capitolo.Id).Include(m => m.Messaggi).FirstOrDefault();
                    User oUser = unitOfWork.Users.Get(User.Identity.GetUserId());

                    Messaggio oMessaggio = new Messaggio();
                    oMessaggio.Submitter = oUser;
                    oMessaggio.DataMessaggio = UtilDate.CurrentTimeMillis();
                    oMessaggio.Testo = capitolo.Messaggi[0].Testo;
                    oMessaggio.Titolo = capitolo.Messaggi[0].Titolo;

                    oCapitolo.Messaggi.Add(oMessaggio);
                    unitOfWork.Complete();

                    log.Debug("addusermessageforcapitolo - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addusermessageforcapitolo - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("getmessaggipercapitolo")]
        public HttpResponseMessage GetMessaggiPerCapitolo([FromBody] Capitolo capitolo)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entity = unitOfWork.Capitoli.Search(u => u.Id == capitolo.Id).Include(m => m.Messaggi.Select(p => p.Risposte.Select(u => u.Utente))).Include(s => s.Messaggi.Select(o => o.Submitter)).FirstOrDefault();
                    log.Debug("getmessaggipercapitolo - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getmessaggipercapitolo - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("addrispostatomessaggio")]
        public HttpResponseMessage AddRispostatoMessaggio([FromBody] Messaggio messaggio)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entityUser = unitOfWork.Users.Get(User.Identity.GetUserId());
                    var entityMessaggio = unitOfWork.Messaggi.Get(messaggio.Id);

                    Risposta oRisposta = new Risposta();
                    oRisposta.DataRisposta = UtilDate.CurrentTimeMillis();
                    oRisposta.Testo = messaggio.Risposte[0].Testo;
                    oRisposta.Messaggio = entityMessaggio;
                    oRisposta.Utente = entityUser;

                    unitOfWork.Risposte.Add(oRisposta);
                    unitOfWork.Complete();
                    //var entity = unitOfWork.Capitoli.Search(u => u.Id == capitolo.Id).Include(m => m.Messaggi.Select(p => p.Risposte)).Include(s => s.Messaggi.Select(o => o.Submitter)).FirstOrDefault();
                    //log.Debug("addrispostatomessaggio - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("addrispostatomessaggio - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
