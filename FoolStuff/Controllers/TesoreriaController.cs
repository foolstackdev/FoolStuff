using FoolStaffDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoolStuff.Controllers
{
    [RoutePrefix("api/tesoreria")]
    public class TesoreriaController : ApiController
    {

        [HttpGet]
        [Route("isAlive")]
        public HttpResponseMessage isAlive()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "I'm Alive, Hello!");
        }

        [HttpGet]
        [Route("getallentry")]
        public HttpResponseMessage getAllEntry()
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    entities.Configuration.ProxyCreationEnabled = false;

                    
                    //var entriesTesoreria = from b in entities.Tesoreria
                    //            where b.Operazione.Equals("VERSAMENTO")
                    //            orderby b.DataOperazione descending
                    //            select b;

                    //var entity = entities.Tesoreria.OrderByDescending(e => e.Operazione.Equals("VERSAMENTO")).ToList();
                    var entity = entities.Tesoreria.Where(x => x.Operazione == "VERSAMENTO").ToList().OrderByDescending(t => t.DataOperazione);
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("getallexit")]
        public HttpResponseMessage getAllExit()
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.Tesoreria.Where(x => x.Operazione == "PRELIEVO").ToList().OrderByDescending(t => t.DataOperazione);
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        

       [HttpPost]
        [Route("insertpayment/{id}")]
        public HttpResponseMessage insertPayment(string id,[FromBody]UserInfo[] users)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    //Tesoreria oTesoreria = new Tesoreria();
                    //oTesoreria.Operazione = "VERSAMENTO";
                    //oTesoreria.DataOperazione = DateTime.Now;
                    //oTesoreria.Note = "Versamento settimanale 5 Euro";

                    //entities.Tesoreria.Add(oTesoreria);
                    ////entities.SaveChanges();
                    int oId = Convert.ToInt32(id);
                    Tesoreria oTesoreria = entities.Tesoreria.FirstOrDefault(e => e.Id == oId);

                    foreach (UserInfo u in users)
                    {
                        User_Tesoreria pagamento = new User_Tesoreria();
                        pagamento.UserInfoId = u.Id;
                        pagamento.Tesoreria = oTesoreria;
                        pagamento.Versamento = 5;

                        entities.User_Tesoreria.Add(pagamento);

                    }
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        [Route("insertpaymentdate")]
        public HttpResponseMessage insertPaymentDate([FromBody]Tesoreria tesoreria)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    Tesoreria oTesoreria = new Tesoreria();
                    oTesoreria.Operazione = "VERSAMENTO";
                    oTesoreria.DataOperazione = tesoreria.DataOperazione;
                    oTesoreria.Note = tesoreria.Note;

                    entities.Tesoreria.Add(oTesoreria);
                    entities.SaveChanges();
                    entities.Configuration.ProxyCreationEnabled = false;
                    //var entity = entities.Tesoreria.OrderByDescending(e => e.Operazione.Equals("VERSAMENTO")).ToList();
                    var entity = entities.Tesoreria.Where(x => x.Operazione == "VERSAMENTO").ToList().OrderByDescending(t => t.DataOperazione);
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
