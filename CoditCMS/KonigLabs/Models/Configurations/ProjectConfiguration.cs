using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            ToTable("Projects")
               .HasKey(p => p.Id);

            HasMany(p => p.Files)
               .WithMany(p => p.Projects)
               .Map(p =>
               {
                   p.ToTable("ProjectFiles");
                   p.MapLeftKey("Project_Id");
                   p.MapRightKey("File_Id");
               });

            HasMany(p => p.ProjectCategory)
               .WithMany(p => p.Projects)
               .Map(p =>
               {
                   p.ToTable("ProjectCategoryProjects");
                   p.MapLeftKey("Project_Id");
                   p.MapRightKey("ProjectCategory_Id");
               });
        }
    }
}