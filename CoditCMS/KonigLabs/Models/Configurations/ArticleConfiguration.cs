using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            ToTable("Articles")
                 .HasKey(p => p.Id);

            HasMany(p => p.Files)
                .WithMany(p => p.Articles)
                .Map(p =>
                {
                    p.ToTable("FileArticles");
                    p.MapLeftKey("Article_Id");
                    p.MapRightKey("File_Id");
                });

            HasMany(p => p.Tags)
              .WithMany(p => p.Articles)
              .Map(p =>
              {
                  p.ToTable("TagArticles");
                  p.MapLeftKey("Tag_Id");
                  p.MapRightKey("File_Id");
              });

            HasMany(p => p.Categories)
              .WithMany(p => p.Articles)
              .Map(p =>
              {
                  p.ToTable("ArticleCategories");
                  p.MapLeftKey("Article_Id");
                  p.MapRightKey("ArticleCategory_Id");
              });
        }
    }
}