using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using FoolStuff.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FoolStaffDataAccess;
using System.Web.Http;

[assembly: OwinStartup(typeof(FoolStuff.Startup))]

namespace FoolStuff
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
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


                //create default user
                var user = new ApplicationUser();
                user.UserName = "admin@admin.it";
                user.Email = "admin@admin.it";
                string pwd = "F00lStack101!";

                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "SuperAdmin");
                }

                UserInfo oUser = new UserInfo();
                oUser.Name = "Admin";
                oUser.Surname = "Admin";
                oUser.Phone = "555";
                oUser.Email = user.Email;
                oUser.Password = pwd;
                oUser.Id = user.Id;

                using (FoolStaffDataModelContainer entities = new FoolStaffDataModelContainer())
                {
                    var entity = entities.UserInfo.FirstOrDefault(u => u.Email == user.Email);

                    if (entity != null)
                    {
                        entities.UserInfo.Add(oUser);
                    }
                    else
                    {
                        entity.Id = user.Id;
                    }
                    entities.SaveChanges();
                }

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
