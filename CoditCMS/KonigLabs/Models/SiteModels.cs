using DB.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Entity;

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

        public string Signature { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public override string ToString()
        {
            return "Участник команды";
        }

        public string GetAvatar()
        {
            var answer = "";
            var file = Files.OrderBy(f => f.Sort).FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal static IEnumerable<CrewMember> GetMembers(string language, ApplicationDbContext db)
        {
            return db.CrewMembers.Include(m => m.Files).Where(m => m.Language == language && m.Visibility).OrderBy(c=>c.Sort).ToList();
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
        public virtual ICollection<Article> Articles { get; set; }

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

        public override string ToString()
        {
            return "Категория проекта";
        }

    }

    public class Project :LocalEntity, IVisibleEntity, ISortableEntity, IMetadataEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<ProjectCategory> ProjectCategory { get; set; }

        public override string ToString()
        {
            return "Проект";
        }
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
            var file = Files.OrderBy(f => f.Sort).FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal string GetBigImage()
        {
            var answer = "";
            var file = Files.OrderBy(f => f.Sort).Skip(1).FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal static IEnumerable<Project> GetProjects(string language, ApplicationDbContext db)
        {
            var projects = new List<Project>();
            foreach (var pc in db.ProjectCategories.Include(p => p.Projects)
                                                    .Include(p => p.Projects.Select(ip => ip.Files))
                                                    .Where(pc => pc.Language == language && pc.Visibility))
            {
                foreach (var pro in pc.Projects.Where(p =>p.Language==language && p.Visibility))
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
            foreach (var file in Files.OrderBy(f => f.Sort).Skip(1))
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

        public override string ToString()
        {
            return "Клиент";
        }

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
            var file = Files.OrderBy(f => f.Sort).FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal static IEnumerable<Client> GetClients(string language, ApplicationDbContext db)
        {
            return db.Clients.Include(cl => cl.Files).Where(cl => cl.Language == language && cl.Visibility).ToArray();
        }
    }

    public class Article : LocalEntity, IVisibleEntity, ISortableEntity, IMetadataEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string EmbeddedMedia { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public bool ShowOnHomePage { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public bool Visibility { get; set; }

        public int Sort { get; set; }


        public virtual CrewMember CrewMember { get; set; }
        public int? CrewMemberId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<ArticleCategory> Categories { get; set; }

        public override string ToString()
        {
            return "Статья/Пост в блог";
        }


        internal string GetSmallImage()
        {
            var answer = "";
            var file = Files.OrderBy(f => f.Sort).FirstOrDefault();
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        internal static IEnumerable<Article> GetArticles(string language, ApplicationDbContext db)
        {
            return db.Articles.Include(a => a.Files).Include(a => a.Categories).Include(a => a.Tags).Include(a => a.CrewMember)
                .Where(a => a.Language == language && a.Visibility).ToArray();
        }


        internal static IEnumerable<Article> GetArticlesForMainPage(string language, ApplicationDbContext db)
        {
            return db.Articles.Where(a => a.ShowOnHomePage).Include(a => a.Files).Include(a => a.Categories).Include(a => a.Tags).Include(a => a.CrewMember)
                .Where(a => a.Language == language && a.Visibility).ToArray();
        }


        public bool HasImageForBlog()
        {
            var file = Files.OrderBy(f => f.Sort).FirstOrDefault();
            if(ShowOnHomePage)
            {
                file = Files.OrderBy(f => f.Sort).Skip(1).FirstOrDefault();
            }
            return file != null;
        }

        public string GetBlogImage()
        {
            var answer = "";
            var file = Files.OrderBy(f => f.Sort).FirstOrDefault();
            if (ShowOnHomePage)
            {
                file = Files.OrderBy(f => f.Sort).Skip(1).FirstOrDefault();
            }                       
            if (file != null)
            {
                answer = file.Name;
            }
            return answer;
        }

        public string GetDisplayDate()
        {

            var ci = "ru-RU";
            if (Language == LocalEntity.EN)
            {
                ci = "en-US";
            }
            return Date.ToString("M", new CultureInfo(ci));
        }


        public string GetSpoiler()
        {
            var sb = new StringBuilder();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(String.Format(@"<html><body>{0}</html>", Content));
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
            {
                sb.Append(node.InnerText);
            }
            var le = 180;
            if (sb.Length < 180)
            {
                le = sb.Length - 1;
            }
            return sb.ToString().Substring(0, le);
        }

        public object CommentsCount { get; set; }
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

        public override string ToString()
        {
            return "Входящий контакт";
        }
    }

    public class Tag : IVisibleEntity, ISortableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Visibility { get; set; }

        public int Sort { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

        public override string ToString()
        {
            return "Тег";
        }
    }

    public class ArticleCategory : IVisibleEntity, ISortableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Visibility { get; set; }

        public int Sort { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

        public override string ToString()
        {
            return "Категория статей/постов";
        }
    }

    public class Comment : IVisibleEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public bool Visibility { get; set; }

        public virtual Article Article { get; set; }
        public int ArticleId { get; set; }

        public virtual CrewMember CrewMember { get; set; }
        public int? CrewMemberId { get; set; }

        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public const string EmptyAvatar = "~/Content/media/img/avatar.png";

        public override string ToString()
        {
            return "Комментарий";
        }
        [NotMapped]
        public string AdminName
        {
            get
            {

                var title = Article.Title;
                if (Parent != null)
                {
                    title += String.Format("  <-  ...   {0} <- {1}", Parent.GetName(), GetName());
                }
                else
                {
                    title += "  <-  " + GetName();
                }
                return title;
            }
        }

        public string GetAvatar()
        {
            var answer = EmptyAvatar;
            if (CrewMember != null)
            {
                answer = CrewMember.GetAvatar();
            }
            return answer;
        }

        public string GetReplyWord()
        {
            var answer = "ответить";
            if (Article.Language == LocalEntity.EN)
            {
                answer = "reply";
            }
            return answer;
        }

        public string GetName()
        {
            var name = Name;
            if (CrewMember != null)
            {
                name = CrewMember.FirstName + " " + CrewMember.LastName;
            }
            return name;
        }

        public string GetDate()
        {
            var ci = "ru-RU";
            if (Article.Language == LocalEntity.EN)
            {
                ci = "en-US";
            }
            return Date.ToString("M", new CultureInfo(ci));
        }

        [NotMapped]
        public int Level { get; set; }
        [NotMapped]
        public int CommentCount { get; set; }


        internal void WalkDawn(int level)
        {
            Level = level;
            CommentCount = 0;
            foreach (var c in Comments.Where(c => c.Visibility))
            {
                c.WalkDawn(level + 1);
                CommentCount += c.CommentCount + 1;

            }            
        }
    }
}