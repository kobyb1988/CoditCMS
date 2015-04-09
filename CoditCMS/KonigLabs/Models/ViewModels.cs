using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace KonigLabs.Models
{
    public class LandingPage
    {
        private string language;
        private ApplicationDbContext db;

        public LandingPage(string language, ApplicationDbContext db)
        {
            // TODO: Complete member initialization
            this.language = language;


            Members = new List<ViewMember>();
            Projects = new List<ViewProject>();
            Categories = new List<ViewCategory>();
            Clients = new List<ViewClient>();
            Articles = new List<ViewArticle>();

            foreach (var member in CrewMember.GetMembers(language, db))
            {
                var me = new ViewMember(member);
                if (String.IsNullOrEmpty(me.Avatar))
                {
                    continue;
                }
                Members.Add(me);
            }

            var cats = new Dictionary<string, ViewCategory>();

            foreach (Project project in Project.GetProjects(language, db))
            {
                var viewProject = new ViewProject(project);
                if (String.IsNullOrEmpty(viewProject.SmallImage) || String.IsNullOrEmpty(viewProject.BigImage))
                {
                    continue;
                }
                Projects.Add(viewProject);
                foreach (var vc in viewProject.Categories)
                {
                    if (!cats.ContainsKey(vc.Token))
                    {
                        cats.Add(vc.Token, vc);
                    }
                    else
                    {
                        vc.Count += 1;
                    }
                }

            }
            Categories = cats.Values.ToList();

            foreach (Client cl in Client.GetClients(language, db))
            {
                
                var vc = new ViewClient(cl);
                if (String.IsNullOrEmpty(vc.Logo))
                {
                    continue;
                }
                Clients.Add(vc);
            }


            foreach (Article ar in Article.GetArticles(language, db))
            {
                var va = new ViewArticle(ar);
                if (String.IsNullOrEmpty(va.Image))
                {
                    continue;
                }
                Articles.Add(va);
            }
            Contact = new ViewContact();
        }
        public List<ViewMember> Members { get; set; }
        public List<ViewCategory> Categories { get; set; }
        public List<ViewProject> Projects { get; set; }
        public List<ViewClient> Clients { get; set; }
        public List<ViewArticle> Articles { get; set; }
        public ViewContact Contact { get; set; }

    }

    public class ViewCategory
    {

        public string Token { get; set; }
        public string Name { get; set; }
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
            foreach (var cat in project.ProjectCategory.ToArray())
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
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public string Bio { get; set; }

        public ViewMember(CrewMember member)
        {
            Avatar = member.GetAvatarPath();
            Title = member.Title;
            FirstName = member.FirstName;
            Name = member.FirstName + " " + member.LastName;
            Bio = member.Bio;
            Id = member.Id;
        }
    }

    public class ViewClient
    {
        public string Logo { get; set; }
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

    public class ViewContact
    {

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Неверный Email адрес")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Сообщение")]
        public string Text { get; set; }

        public string Status { get; set; }
    }
}