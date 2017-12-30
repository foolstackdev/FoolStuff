using FoolStaff;
using FoolStaff.Core.Domain;
using FoolStuff.Dto;
using FoolStuff.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace FoolStuff.Controllers
{

    //[Authorize]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [HttpGet]
        [Route("isalive")]
        public HttpResponseMessage isAlive()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "I'm Alive, Hello!");
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getalluserswithroles")]
        public IHttpActionResult getAllUsersWithRoles()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                context.Configuration.ProxyCreationEnabled = false;

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //var bbb = (from user in context.Users
                //           from userRole in user.Roles
                //           join role in context.Roles
                //           on userRole.RoleId equals role.Id
                //           select user);

                //var aaa = (from u in context.Users
                //           join r in context.Roles
                //            on u.Roles equals r.Id
                //           select u);

                //var uuu = context.Users.Where
                //var usersWithRoles = context.Users.Select(x => new UserWithRolesViewModel { User = x, UserRoles = x.Roles }).ToList();
                var allUsers = userManager.Users.ToList();
                var users = allUsers.Select(u => new UsersViewModel {
                    User = u,
                    Roles = String.Join(",", roleManager.Roles.Where(role => role.Users.Any(user => user.UserId == u.Id)).Select(r => r.Name)),
                    UserInfo = new UnitOfWork(new FoolStaffContext()).Users.Find(us => us.Id == u.Id).SingleOrDefault()
                }).ToList();
                //var users = userManager.Users.Include(t => t.Roles).ToList();
                // var roles = this.AppRoleManager.Roles.ToList();


                log.Debug("getalluserswithroles - metodo eseguito con successo");
                return Ok(users);

            }
            catch (Exception ex)
            {
                log.Error("getalluserswithroles - errore nell'esecuzione", ex);
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getroleslist")]
        public IHttpActionResult getRolesList()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleMngr = new RoleManager<IdentityRole>(roleStore);
                var roles = roleMngr.Roles.ToList();
                log.Debug("getroleslist - metodo eseguito con successo");
                return Ok(roles);

            }
            catch (Exception ex)
            {
                log.Error("getroleslist - errore nell'esecuzione", ex);
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("addrole")]
        public IHttpActionResult addRole([FromBody]RoleBindingModels newRole)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists(newRole.Name))
                {
                    var role = new IdentityRole(newRole.Name);
                    roleManager.Create(role);
                    log.Debug("addrole - ruolo + [" + newRole.Name + "] aggiunto con successo");
                    return Ok(role);
                }
                else
                {
                    log.Error("addrole - ruolo + [" + newRole.Name + "] già esistente");
                    return BadRequest("Role [" + newRole.Name + "] already profiled");
                }
            }
            catch (Exception ex)
            {
                log.Error("addrole - errore nell'esecuzione");
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("changerole")]
        public IHttpActionResult changeRole([FromBody]RolesUpdateRuolo ruolo)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (roleManager.RoleExists(ruolo.role))
                {
                    userManager.RemoveFromRole(ruolo.userId, ruolo.oldrole);
                    userManager.AddToRole(ruolo.userId, ruolo.role);
                }
                else
                {
                    string sMessage = "Nessun ruolo trovato col nome [" + ruolo.role + "]";
                    log.Error(sMessage);
                    throw new Exception(sMessage);
                }
            }
            catch (Exception ex)
            {
                log.Error("changeRole - errore nell'esecuzione", ex);
                return InternalServerError(ex);
            }
            return Ok();
        }
        public class UsersViewModel
        {
            [Display(Name = "User")]
            public ApplicationUser User { get; set; }

            [Display(Name = "Roles")]
            public string Roles { get; set; }

            [Display(Name = "UserInfo")]
            public User UserInfo { get; set; }
        }
        //private class UserWithRolesViewModel
        //{
        //    public ApplicationUser User { get; set; }
        //    public ICollection<IdentityUserRole> UserRoles { get; set; }
        //}
    }

}
