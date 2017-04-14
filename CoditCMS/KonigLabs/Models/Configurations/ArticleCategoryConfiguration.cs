using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class ArticleCategoryConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleCategoryConfiguration()
        {
            ToTable("ArticleCategories")
            .HasKey(p => p.Id);
        }
    }
}