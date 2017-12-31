using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using System.Security.Claims;
using FoolStaff;
using FoolStaff.Core.Domain;
using FoolStuff.Dto;
using FoolStuff.Manager;

namespace FoolStuff.Controllers
{
    [Authorize]
    [RoutePrefix("api/useraccount")]
    public class UseraccountController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //entities.Configuration.ProxyCreationEnabled = false;
                    var entity = unitOfWork.Users.GetAll().ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpPost]
        [Route("updateuserinfo/{id}")]
        public HttpResponseMessage updateUserInfo(string id, [FromBody]User user)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //entities.Configuration.ProxyCreationEnabled = false;
                    var entity = unitOfWork.Users.SingleOrDefault(u => u.Id == id);
                    if (entity != null)
                    {
                        entity.Name = user.Name;
                        entity.Surname = user.Surname;
                        entity.Phone = user.Phone;
                        unitOfWork.Users.Add(entity);
                        unitOfWork.Complete();
                        log.Debug("allUsers - eseguito con successo");
                        return Request.CreateResponse(HttpStatusCode.OK, entity);

                    }
                    else
                    {
                        log.Error("allUsers - errore nell'esecuzione ");
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id [" + user.Id + "] not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("allUsers - errore nell'esecuzione ");
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
                using (var unitOfWork = new UnitOfWork(new FoolStaffContext()))
                {
                    //entities.Configuration.ProxyCreationEnabled = false;
                    var entity = unitOfWork.Users.SingleOrDefault(u => u.Email == email);

                    ////////////////////////RUOLO
                    //PRIMO SISTEMA LIST<string>
                    var userIdentity = (ClaimsIdentity)User.Identity;
                    var sListaRuoli = userIdentity.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .ToList();
                    //END PRIMO SISTEMA

                    //SECONDO SISTEMA OGGETO PIU COMPLESSO
                    //var userIdentity = (ClaimsIdentity)User.Identity;
                    //var claims = userIdentity.Claims;
                    //var roleClaimType = userIdentity.RoleClaimType;
                    //var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

                    //alternativamente
                    //var roles = claims.Where(c => c.Type == roleClaimType).ToList();
                    //END SECONDO SISTEMA
                    ////////////////////////END RUOLO

                    if (entity != null)
                    {
                        log.Debug("getuserinfo - L'utente con email " + email + "loggato con successo");
                        UserInfoWithRoles oUserInfoWithRoles = new UserInfoWithRoles();
                        oUserInfoWithRoles.userInfo = entity;
                        oUserInfoWithRoles.userRolesList = sListaRuoli;
                        oUserInfoWithRoles.userAvatar = new Avatar().getAvatarByUser(entity.Id);
                        //return Request.CreateResponse(HttpStatusCode.OK, entity);
                        return Request.CreateResponse(HttpStatusCode.OK, oUserInfoWithRoles);
                    }
                    else
                    {
                        log.Error("getuserinfo - errore nell'esecuzione ");
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Email [" + email + "] not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("getuserinfo - errore nell'esecuzione ");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        private class UserInfoWithRoles
        {
            public List<string> userRolesList { get; set; }
            public User userInfo { get; set; }
            public List<AvatarImages> userAvatar { get; set; }
            public UserInfoWithRoles()
            {

            }
        }
    }
}
