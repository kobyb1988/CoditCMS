using DB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace KonigLabs.Models
{

    public class LocalEntity
    {
        public const string RU = "ru";
        public const string EN = "en";

        [MaxLength(2)]
        [Index("Language")]
        public string Language { get; set; }
    }

    public class CrewMember : LocalEntity, IVisibleEntity, ISortableEntity, IMetadataEntity
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
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal static IEnumerable<CrewMember> GetMembers(string language, ApplicationDbContext db)
        {
            return db.CrewMembers.Include("Files").Where(m => m.Language == language && m.Visibility).ToList();
        }
    }


    public class File : IFileEntity
    {

        public global::System.Int32 Id { get; set; }

        public global::System.String Name { get; set; }

        public global::System.String SourceName { get; set; }

        public global::System.Boolean Visibility { get; set; }

        public global::System.Int32 Sort { get; set; }

        public global::System.DateTime Date { get; set; }

        public Nullable<global::System.Int64> Size { get; set; }

        public global::System.String Alt { get; set; }

        public global::System.String Title { get; set; }

        public global::System.String Description { get; set; }

        public virtual ICollection<CrewMember> Members { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

    }

    public class ProjectCategory : LocalEntity, IVisibleEntity, ISortableEntity, IMetadataEntity
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
        public string Description { get; set; }

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

        internal static IEnumerable<Project> GetProjects(string language, ApplicationDbContext db)
        {
            var projects = new List<Project>();
            foreach (var pc in db.ProjectCategories.Include("Projects").Include("Projects.Files").Where(pc => pc.Language == language && pc.Visibility))
            {
                foreach (var pro in pc.Projects.Where(p=>p.Visibility))
                {
                    projects.Add(pro);
                }
            }
            //var projects = new List<Project>();
            //var categories = 
            //db.Projects.Include("ProjectCategory").Include("Files").Where(p => p.ProjectCategory).ToArray();
            return projects;
        }

        internal List<string> GetImages()
        {
            var answer = new List<string>();
            foreach(var file in Files.Skip(1))
            {
                answer.Add(file.Name);
            }
            return answer; 
        }
    }


    public class Client : LocalEntity, IVisibleEntity, ISortableEntity, IMetadataEntity
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

        internal string GetLogo()
        {
            var answer = "";
            var file = Files.FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal static IEnumerable<Client> GetClients(string language, ApplicationDbContext db)
        {
            return db.Clients.Include("Files").Where(cl=>cl.Language==language && cl.Visibility).ToArray();
        }
    }

    public class Article : LocalEntity, IVisibleEntity, ISortableEntity, IMetadataEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }

        public virtual ICollection<File> Files { get; set; }

        internal string GetImage()
        {
            var answer = "";
            var file = Files.FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal static IEnumerable<Article> GetArticles(string language, ApplicationDbContext db)
        {
            return db.Articles.Include("Files").Where(a=>a.Language==language && a.Visibility).ToArray();
        }
    }

    public class Contact : IVisibleEntity, ISortableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Text { get; set; }

        public string Phone { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }

        public DateTime Date { get; set; }

    }
}