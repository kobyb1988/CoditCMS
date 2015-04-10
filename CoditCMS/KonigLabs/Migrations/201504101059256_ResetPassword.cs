namespace KonigLabs.Migrations
{
    using KonigLabs.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetPassword : DbMigration
    {
        public override void Up()
        {
            using (var context = ApplicationDbContext.Create())
            {
                ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));                
                var admin = userManager.FindByEmail("admin@koniglabs.ru");
                userManager.RemovePassword(admin.Id);
                userManager.AddPassword(admin.Id, "fake password");                
            }
        }
        
        public override void Down()
        {
        }
    }
}
