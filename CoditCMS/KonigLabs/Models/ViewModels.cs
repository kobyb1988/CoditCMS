using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KonigLabs.Models
{
    public class LandingPage
    {
        public List<ViewMember> Members { get; set; }
        public List<ViewCategory> Categories { get; set; }
        public List<ViewProject> Projects { get; set; }
        public List<ViewClient> Clients { get; set; }
        public List<ViewArticle> Articles { get; set; }    
    }

    public class ViewCategory
    {
        
        public string Token {get;set;}
        public string Name {get;set;}
        public int Count { get; set; }

        public ViewCategory(ProjectCategory category)
        {
            Token = MakeToken(category);
            Name = category.Name;
            Count = 1;
        }
        public static string MakeToken(ProjectCategory cat)
        {
            return String.Format("category-{0}", cat.Id);
        }
    }

    public class ViewProject
    {
        public string Name { get; set; }
        public List<ViewCategory> Categories { get; set; }
        public string BigImage { get; set; }
        public string SmallImage { get; set; }
        public int Id { get; set; }

        public ViewProject(Project project)
        {
            Id = project.Id;
            SmallImage = project.GetSmallImage();
            BigImage = project.GetBigImage();
            Categories = new List<ViewCategory>();
            Name = project.Name;
            foreach(var cat in project.ProjectCategory.ToArray())
            {
                Categories.Add(new ViewCategory(cat));
            }
        }


        public string GetFilterClasses()
        {
            var sb = new StringBuilder();
            foreach (var cat in Categories)
            {
                sb.AppendFormat(" {0}", cat.Token);
            }
            return sb.ToString();
        }
    }

    public class ViewMember
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public ViewMember(CrewMember member)
        {
            Avatar = member.GetAvatarPath();
            Title = member.Title;
        }
    }

    public class ViewClient
    {
        public string Logo { get; set;}
        public string Title { get; set; }

        public ViewClient(Client client)
        {
            Logo = client.GetLogo();
            Title = client.Title;
        }
    }

    public class ViewArticle
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }

        public ViewArticle(Article article)
        {
            Image = article.GetImage();
            Title = article.Title;
            Date = article.Date.ToString("d MMMM yyyy");
        }

        
    }

}