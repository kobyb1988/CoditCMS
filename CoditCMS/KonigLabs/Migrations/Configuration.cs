using KonigLabs.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KonigLabs.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KonigLabs.Models.ApplicationDbContext context)
        {
                //ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                //var roleResult = roleManager.Create(new IdentityRole() { Name = CMS.PagesSettings.Settings.AdminRole });
                //Console.WriteLine(roleResult.Succeeded.ToString());

                //var user = new ApplicationUser { Email = username, UserName = username };
                //var userResult = ApplicationUserManager.Create(user, password);

                //Console.WriteLine(userResult.Succeeded.ToString());
                //if (userResult.Succeeded)
                //{
                //    userManager.AddToRole(user.Id, "admin");
                //}
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
