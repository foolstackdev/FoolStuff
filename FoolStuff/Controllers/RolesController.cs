using FoolStaffDataAccess;
using FoolStuff.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("getallroles")]
        public IHttpActionResult getAllEntry()
        {
            try
            {
                //ApplicationDbContext context = new ApplicationDbContext();
                //context.Configuration.ProxyCreationEnabled = false;

                //var bbb = (from user in context.Users
                //           from userRole in user.Roles
                //           join role in context.Roles 
                //           on userRole.RoleId equals role.Id
                //           select user);



                //var aaa = (from u in context.Users join r in context.Roles
                //           on u.Roles equals r.Id
                //           select u);

                //var uuu = context.Users.Where


                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                //var users = userManager.Users.Include(t => t.Roles).ToList();
                var roles = this.AppRoleManager.Roles.ToList();



                return Ok(roles);

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

    }

}
