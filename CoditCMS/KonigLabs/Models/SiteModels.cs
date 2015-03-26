using DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace KonigLabs.Models
{
    public class CrewMember: IVisibleEntity, ISortableEntity, IMetadataEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public string GetAvatarPath()
        {
            return Files.FirstOrDefault().Name;
        }
    }

   
    public class File : IFileEntity
    {
              
        public global::System.Int32 Id {get;set;}
        
        public global::System.String Name {get;set;}
        
        public global::System.String SourceName {get;set;}
        
        public global::System.Boolean Visibility {get;set;}
        
        public global::System.Int32 Sort {get;set;}
        
        public global::System.DateTime Date {get;set;}
        
        public Nullable<global::System.Int64> Size {get;set;}
        
        public global::System.String Alt {get;set;}
        
        public global::System.String Title {get;set;}
        
        public global::System.String Description {get;set;}

        public virtual ICollection<CrewMember> Members { get; set; }
        public virtual ICollection<Project> Projects { get; set; } 

    }

    public class ProjectCategory : IVisibleEntity, ISortableEntity, IMetadataEntity, INestedEntity<ProjectCategory>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public bool Visibility { get; set; }

        public int Sort { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public Nullable<int> ProjectCategoryID { get; set; }
        public virtual ICollection<ProjectCategory> SubCategories { get; set; }

        [NotMapped]
        public virtual ProjectCategory Parent { get; set; }
        [NotMapped]
        public int? ParentId { get; set; }
        [NotMapped]
        public int Level { get; set; }
        [NotMapped]
        public bool HasChilds { get; set; }

        public EntityCollection<ProjectCategory> Children
        {
            get
            {
                var entityCollection = new EntityCollection<ProjectCategory>();
                if (SubCategories != null)
                {
                    foreach (var cat in SubCategories)
                    {
                        entityCollection.Add(cat);
                    }
                }
                return entityCollection;
            }
            set { }
        }
    }

    public class Project : IVisibleEntity, ISortableEntity, IMetadataEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description{ get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }

        public virtual ICollection<File> Files { get; set; }
        
        public int ProjectCategoryID { get; set; }
    }
}