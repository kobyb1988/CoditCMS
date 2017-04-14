using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class ProjectCategoryConfiguration : EntityTypeConfiguration<ProjectCategory>
    {
        public ProjectCategoryConfiguration()
        {
            ToTable("ProjectCategories")
           .HasKey(p => p.Id);
        }
    }
}