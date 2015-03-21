namespace KonigLabs.Migrations
{
    using KonigLabs.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    public partial class AdminRole : DbMigration
    {
        public const string username = "admin@koniglabs.ru";
        public const string password = "aq1sw2";

        public override void Up()
        {
            
            using (var context = ApplicationDbContext.Create())
            {
                ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleResult = roleManager.Create(new IdentityRole() { Name = CMS.PagesSettings.Settings.AdminRole });
                Console.WriteLine(roleResult.Succeeded.ToString());

                var user = new ApplicationUser { Email = username, UserName = username };
                var userResult = userManager.Create(user, password);

                Console.WriteLine(userResult.Succeeded.ToString());
                if (userResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "admin");
                }
            }
        }

        public override void Down()
        {
        }
    }
}
