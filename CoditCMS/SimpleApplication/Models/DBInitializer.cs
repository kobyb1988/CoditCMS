using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleApplication.Models
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var simple = "simple";
            simple += " and even simpler";
            simple += " and super simple";
        }
    }
}