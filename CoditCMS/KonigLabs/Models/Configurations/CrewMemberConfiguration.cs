using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace KonigLabs.Models.Configurations
{
    public class CrewMemberConfiguration : EntityTypeConfiguration<CrewMember>
    {
        public CrewMemberConfiguration()
        {
            ToTable("CrewMembers")
             .HasKey(p => p.Id);

            HasMany(p => p.Files)
                .WithMany(p => p.Members)
                 .Map(p =>
                 {
                     p.ToTable("FileCrewMembers");
                     p.MapLeftKey("CrewMember_Id");
                     p.MapRightKey("File_Id");
                 });

            //HasMany(p => p.Articles)
            //    .WithOptional(p => p.CrewMember)
            //    .HasForeignKey(p => p.CrewMemberId);

            //HasMany(p => p.Comments)
            //    .WithOptional(p => p.CrewMember)
            //    .HasForeignKey(p => p.CrewMemberId);

            //HasOptional(s => s.SmallPhoto)
            // .WithRequired(p => p.Member);
        }
    }
}