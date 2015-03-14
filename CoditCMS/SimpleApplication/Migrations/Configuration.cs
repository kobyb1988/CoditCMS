namespace SimpleApplication.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SimpleApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SimpleApplication.Models.ApplicationDbContext";
        }

        protected override void Seed(SimpleApplication.Models.ApplicationDbContext context)
        {

            //var roleName = "admin";

          
            //ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var roleResult = roleManager.Create(new IdentityRole() { Name = roleName });
            //Console.WriteLine(roleResult.Succeeded.ToString());

            //var user = new ApplicationUser { Email = "aganzha@yandex.ru", UserName = "aganzha@yandex.ru" };
            //var userResult = userManager.Create(user, "aganzha@yandex.ru");

            //Console.WriteLine(userResult.Succeeded.ToString());
            //if (userResult.Succeeded)
            //{
            //    userManager.AddToRole(user.Id, "admin");
            //}

        }
    }
}
