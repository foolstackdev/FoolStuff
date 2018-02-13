using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using FoolStuff.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;
using FoolStaff.Core.Domain;
using FoolStaff;

[assembly: OwinStartup(typeof(FoolStuff.Startup))]

namespace FoolStuff
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles()
        {
            
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("SuperAdmin"))
            {
                //create super admin role
                var role = new IdentityRole("SuperAdmin");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("SimpleUser"))
            {
                var role = new IdentityRole("SimpleUser");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("FoolStackUser"))
            {
                var role = new IdentityRole("FoolStackUser");
                roleManager.Create(role);
            }
            
        }

    }
}
