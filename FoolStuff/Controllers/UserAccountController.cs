using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoolStaffDataAccess;

namespace FoolStuff.Controllers
{
    [RoutePrefix("api/useraccount")]
    public class UseraccountController : ApiController
    {
        [HttpGet]
        [Route("isAlive")]
        public HttpResponseMessage isAlive()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "I'm Alive, Hello!");
        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register([FromBody]User user)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    entities.Users.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[BasicAuthentication]
        //[Authorize]
        [HttpGet]
        [Route("allusers")]
        public HttpResponseMessage allUsers()
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.Users.ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        [Authorize]
        [HttpPost]
        [Route("updateuserinfo")]
        public HttpResponseMessage updateUserInfo([FromBody]User user)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    var entity = entities.Users.FirstOrDefault(u => u.Email == user.Email);
                    if (entity != null)
                    {
                        entity.Name = user.Name;
                        entity.Surname = user.Surname;
                        entity.Phone = user.Phone;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Email [" + user.Email + "] not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        [HttpPost]
        [Route("insertpayment")]
        public HttpResponseMessage insertPayment([FromBody]User[] users)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    Tesoreria oTesoreria = new Tesoreria();
                    oTesoreria.Operazione = "VERSAMENTO";
                    oTesoreria.DataOperazione = DateTime.Now;
                    oTesoreria.Note = "Versamento settimanale 5 Euro";

                    entities.Tesoreria.Add(oTesoreria);
                    //entities.SaveChanges();

                    foreach (User u in users)
                    {
                        User_Tesoreria pagamento = new User_Tesoreria();
                        pagamento.User = u;
                        pagamento.Tesoreria = oTesoreria;
                        pagamento.Versamento = 5;

                        entities.User_Tesoreria.Add(pagamento);

                    }
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);



                    //var entity = entities.Users.FirstOrDefault(u => u.Email == user.Email);
                    //if (entity != null)
                    //{
                    //    entity.Name = user.Name;
                    //    entity.Surname = user.Surname;
                    //    entity.Phone = user.Phone;

                    //    entities.SaveChanges();
                    //    return Request.CreateResponse(HttpStatusCode.OK, entity);
                    //}
                    //else
                    //{
                    //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Email [" + user.Email + "] not found.");
                    //}
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
    }
}
