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
            return Request.CreateResponse(HttpStatusCode.OK, "I'm Alive");
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
