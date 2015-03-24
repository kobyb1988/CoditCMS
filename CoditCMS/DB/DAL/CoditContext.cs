using DB.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DB.DAL
{


    public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext()
                : base("DefaultConnection")
            {
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }
            public virtual DbSet<SiteSetting> SiteSettings { get; set; }
            public virtual DbSet<EmailLog> EmailLogs { get; set; }
            public virtual DbSet<Location> Location { get; set; }            
        }

}
