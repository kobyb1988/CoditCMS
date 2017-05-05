using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class ClientsConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientsConfiguration()
        {
            ToTable("Clients")
          .HasKey(p => p.Id);
        }
    }
}