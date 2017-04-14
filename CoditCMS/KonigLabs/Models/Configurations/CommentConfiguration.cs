using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            ToTable("Comments")
             .HasKey(p => p.Id);

            HasMany(p => p.Comments)
              .WithOptional(p => p.Parent)
              .HasForeignKey(p => p.ParentId);
        }
    }
}