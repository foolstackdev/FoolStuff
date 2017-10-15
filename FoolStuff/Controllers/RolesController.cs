using FoolStaffDataAccess;
using FoolStuff.Models;
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

namespace FoolStuff.Controllers
{
    //[Authorize]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {

        [HttpGet]
        [Route("isalive")]
        public HttpResponseMessage isAlive()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "I'm Alive, Hello!");
        }

        [Authorize(Roles = "SuperAdmin, FoolStackUser")]
        [HttpGet]
        [Route("getallroles")]
        public IHttpActionResult getAllEntry()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                context.Configuration.ProxyCreationEnabled = false;

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var bbb = (from user in context.Users
                           from userRole in user.Roles
                           join role in context.Roles
                           on userRole.RoleId equals role.Id
                           select user);



                //var aaa = (from u in context.Users
                //           join r in context.Roles
                //            on u.Roles equals r.Id
                //           select u);

                //var uuu = context.Users.Where
                //var usersWithRoles = context.Users.Select(x => new UserWithRolesViewModel { User = x, UserRoles = x.Roles }).ToList();
                var allUsers = userManager.Users.ToList();
                var users = allUsers.Select(u => new UsersViewModel { User = u, Roles = String.Join(",", roleManager.Roles.Where(role => role.Users.Any(user => user.UserId == u.Id)).Select(r => r.Name)) }).ToList();
                //var users = userManager.Users.Include(t => t.Roles).ToList();
               // var roles = this.AppRoleManager.Roles.ToList();



                return Ok(users);

            }
            catch (Exception ex)
            {
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
                    return Ok(role);
                }
                else
                {
                    return BadRequest("Role [" + newRole.Name + "] already profiled");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public class UsersViewModel
        {
            [Display(Name = "User")]
            public ApplicationUser User { get; set; }

            [Display(Name = "Roles")]
            public string Roles { get; set; }
        }
        //private class UserWithRolesViewModel
        //{
        //    public ApplicationUser User { get; set; }
        //    public ICollection<IdentityUserRole> UserRoles { get; set; }
        //}
    }

}
