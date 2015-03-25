using DB.Entities;
using System;
using System.Collections.Generic;
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

    }
}