using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class DataModelContext : DbContext
    {

        public DataModelContext()
            : base("DataModelContext")
        {
        }

        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
