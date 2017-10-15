using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoolStaffDataAccess;

namespace FoolStuff.Controllers
{
    [Authorize]
    [RoutePrefix("api/useraccount")]
    public class UseraccountController : ApiController
    {
        [HttpGet]
        [Route("isalive")]
        public HttpResponseMessage isAlive()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "I'm Alive, Hello!");
        }

        //20171008 Old Method to delete if unused
        //[HttpPost]
        //[Route("register")]
        //public HttpResponseMessage Register([FromBody]UserInfo user)
        //{
        //    try
        //    {
        //        using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
        //        {
        //            entities.UserInfo.Add(user);
        //            entities.SaveChanges();

        //            var message = Request.CreateResponse(HttpStatusCode.Created, user);
        //            return message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[BasicAuthentication]]
        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("allusers")]
        public HttpResponseMessage allUsers()
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    //entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.UserInfo.ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("updateuserinfo/{id}")]
        public HttpResponseMessage updateUserInfo(string id, [FromBody]UserInfo user)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    //entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.UserInfo.FirstOrDefault(u => u.Id == id);
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
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id [" + user.Id + "] not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser, SimpleUser")]
        [HttpGet]
        [Route("getuserinfo/{email}")]
        public HttpResponseMessage getUserInfo(string email)
        {
            try
            {
                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.UserInfo.FirstOrDefault(u => u.Email == email);
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Email [" + email + "] not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
    }
}
