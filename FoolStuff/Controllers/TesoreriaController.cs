using FoolStaff;
using FoolStaff.Core.Domain;
using FoolStaff.Core.Repositories;
using FoolStuff.Dto;
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
    
    [RoutePrefix("api/tesoreria")]
    public class TesoreriaController : ApiController
    {
        private static readonly string SPESA = "SPESA";
        private static readonly string VERSAMENTO = "VERSAMENTO";

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getallentry")]
        public HttpResponseMessage getAllEntry()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entity = unitOfWork.Tesoreria.Search(x => x.Operazione == VERSAMENTO).Include(u => u.user).ToList().OrderByDescending(t => t.DataOperazione);
                    log.Debug("getAllEntry - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getAllEntry - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getallexit")]
        public HttpResponseMessage getAllExit()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    var entity = unitOfWork.Tesoreria.Find(x => x.Operazione == SPESA).ToList().OrderByDescending(t => t.DataOperazione);
                    log.Debug("getAllExit - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                log.Error("getAllExit - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getsaldo")]
        public HttpResponseMessage getSaldo()
        {
            try
            {
                TesoreriaSaldo oTesoreriaSaldo = new TesoreriaSaldo();
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {

                    var entityTesoreriaEntrate = unitOfWork.Tesoreria.Search(x => x.Operazione == VERSAMENTO).Include(u => u.user);
                    var entityTesoreriaUscite = unitOfWork.Tesoreria.Search(x => x.Operazione == SPESA);

                    foreach(Tesoreria oTesoreria in entityTesoreriaEntrate)
                    {
                        foreach(User user in oTesoreria.user)
                        {
                            oTesoreriaSaldo.totaleEntrate += oTesoreria.Quota;
                        }
                    }
                    foreach(Tesoreria oTesoreria in entityTesoreriaUscite)
                    {
                        oTesoreriaSaldo.totaleUscite += oTesoreria.Quota;
                    }
                    oTesoreriaSaldo.saldo = (oTesoreriaSaldo.totaleEntrate - oTesoreriaSaldo.totaleUscite);
                    log.Debug("getsaldo - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK, oTesoreriaSaldo);
                }
            }
            catch (Exception ex)
            {
                log.Error("getsaldo - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("insertversamento")]
        public HttpResponseMessage insertVersamento([FromBody]TesoreriaInsertVersamento payment)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    Tesoreria oTesoreria = new Tesoreria();
                    oTesoreria.Operazione = VERSAMENTO;
                    oTesoreria.DataOperazione = payment.dataOperazione;
                    oTesoreria.Note = payment.note;
                    oTesoreria.Quota = payment.quota;

                    foreach (User usr in payment.users)
                    {
                        var user = unitOfWork.Users.Search(u => u.Id == usr.Id).Include(u => u.Tesoreria).FirstOrDefault();
                        user.Tesoreria.Add(oTesoreria);
                    }
                    unitOfWork.Complete();

                    log.Debug("insertVersamento - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("insertVersamento - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("insertspesa")]
        public HttpResponseMessage insertSpesa([FromBody]TesoreriaInsertSpesa payment)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    Tesoreria oTesoreria = new Tesoreria();
                    oTesoreria.Operazione = SPESA;
                    oTesoreria.DataOperazione = payment.dataOperazione;
                    oTesoreria.Note = payment.note;
                    oTesoreria.Quota = payment.quota;

                    unitOfWork.Tesoreria.Add(oTesoreria);

                    unitOfWork.Complete();

                    log.Debug("insertSpesa - metodo eseguito con successo");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                log.Error("insertSpesa - errore nell'esecuzione ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
