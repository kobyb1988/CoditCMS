using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class FileConfiguration : EntityTypeConfiguration<File>
    {
        public FileConfiguration()
        {
            ToTable("Files")
              .HasKey(p => p.Id);

            HasMany(p => p.Clients)
              .WithMany(p => p.Files)
              .Map(p =>
              {
                  p.ToTable("ClientFiles");
                  p.MapLeftKey("File_Id");
                  p.MapRightKey("Client_Id");
              });
        }
    }
}