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
            var answer = "";
            var file = Files.FirstOrDefault();
            if(file != null)
            {
                answer = file.Name;
            }
            return answer;
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
        public virtual ICollection<Client> Clients { get; set; } 

    }

    public class ProjectCategory : IVisibleEntity, ISortableEntity, IMetadataEntity
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

        public virtual ICollection<ProjectCategory> ProjectCategory { get; set; }

        public string GetImage()
        {
            var answer = "";
            var file = Files.FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal string GetSmallImage()
        {
            var answer = "";
            var file = Files.FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal string GetBigImage()
        {
            var answer = "";
            var file = Files.Skip(1).FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }
    }


    public class Client : IVisibleEntity, ISortableEntity, IMetadataEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public string GetAvatarPath()
        {
            var answer = "";
            var file = Files.FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }
    }
}