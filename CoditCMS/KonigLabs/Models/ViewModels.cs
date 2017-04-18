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
        public string Language { get; private set; }

       // private ApplicationDbContext db;

        public const int MaxProjectInCategory = 8;
        public List<int> ShownProjects { get; set; }

        public LandingPage(string language, ApplicationDbContext db)
        {
            // TODO: Complete member initialization
            Language = language;

            Members = new List<ViewMember>();
            Projects = new List<ViewProject>();
            Categories = new List<ViewCategory>();
            Clients = new List<ViewClient>();
            Articles = new List<ViewArticle>();
            ShownProjects = new List<int>();

            foreach (var member in CrewMember.GetMembers(Language, db))
            {
                var me = new ViewMember(member);
                if (String.IsNullOrEmpty(me.Avatar))
                {
                    continue;
                }
                Members.Add(me);
            }

            var cats = new Dictionary<string, ViewCategory>();

            foreach (Project project in Project.GetProjects(Language, db))
            {
                var viewProject = new ViewProject(project);
                if (String.IsNullOrEmpty(viewProject.SmallImage) || String.IsNullOrEmpty(viewProject.BigImage))
                {
                    continue;
                }

                var includeProject = false;

                foreach (var vc in viewProject.Categories)
                {
                    if (!cats.ContainsKey(vc.Token))
                    {
                        cats.Add(vc.Token, vc);
                        includeProject = true;
                    }
                    else
                    {
                        cats[vc.Token].Count += 1;
                        if (cats[vc.Token].Count <= MaxProjectInCategory)
                        {
                            includeProject = true;
                        }
                    }
                }
                if (includeProject && !ShownProjects.Contains(viewProject.Id))
                {
                    Projects.Add(viewProject);
                    ShownProjects.Add(viewProject.Id);
                }
            }
            Categories = cats.Values.ToList();

            foreach (Client cl in Client.GetClients(Language, db))
            {

                var vc = new ViewClient(cl);
                if (String.IsNullOrEmpty(vc.Logo))
                {
                    continue;
                }
                Clients.Add(vc);
            }

            foreach (Article ar in Article.GetArticlesForMainPage(Language, db))
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

        public string GetFilter()
        {
            var answer = Token;
            if (answer != "*")
            {
                answer = "." + answer;
            }
            return answer;
        }
    }

    public class ViewProject
    {
        public string Name { get; set; }
        public List<ViewCategory> Categories { get; set; }
        public string BigImage { get; set; }
        public string SmallImage { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public List<string> Gallery { get; set; }
        public string Url { get; set; }

        public ViewProject(Project project)
        {
            Id = project.Id;
            SmallImage = project.GetSmallImage();
            BigImage = project.GetBigImage();
            Categories = new List<ViewCategory>();
            Gallery = project.GetImages();
            Name = project.Name;
            Description = project.Description;
            Url = project.Url;
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

    public class ViewProjects
    {
        public List<ViewProject> Projects { get; set; }
        public List<int> ShownProjects { get; set; }

        public ViewProjects(string language, ApplicationDbContext db)
        {
            Projects = new List<ViewProject>();
            var landing = new LandingPage(language, db);

            foreach (Project project in Project.GetProjects(language, db))
            {

                var vp = new ViewProject(project);
                if (String.IsNullOrEmpty(vp.SmallImage) || String.IsNullOrEmpty(vp.BigImage))
                {
                    continue;
                }
                if (landing.ShownProjects.Contains(project.Id))
                {
                    continue;
                }
                Projects.Add(vp);
            }

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

        public string AnimateAvatar { get; set; }

        public ViewMember(CrewMember member)
        {
            Avatar = member.GetAvatar();
            Title = member.Title;
            FirstName = member.FirstName;
            Name = member.FirstName + " " + member.LastName;
            Bio = member.Bio;
            Id = member.Id;
            AnimateAvatar = member.GetAnimateAvatar();
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
        public int Id { get; set; }
        public string Content { get; set; }

        public ViewArticle(Article article)
        {
            Image = article.GetSmallImage();
            Title = article.Title;
            Date = article.Date.ToString("d MMMM yyyy");
            Id = article.Id;
            Content = article.Content;
        }

    }

    public class ViewContact
    {

        [Required(ErrorMessageResourceType =typeof(ErrorMsg), ErrorMessageResourceName = "RequiredName")]
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

    public class ViewTag
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public ViewTag(Tag tag, int count)
        {
            Id = tag.Id;
            Name = tag.Name;
            Count = count;
        }
    }

    public class ViewArticleCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public ViewArticleCategory(ArticleCategory category, int count)
        {
            Id = category.Id;
            Name = category.Name;
            Count = count;
        }
    }

    public class BlogMeta
    {
        public List<ViewTag> Tags { get; set; }
        public List<ViewArticleCategory> Categories { get; set; }
        public string SearchValue { get; set; }

        public BlogMeta(ApplicationDbContext db, string[] langugaes, string currentLang)
        {
            SearchValue = "";
            Tags = new List<ViewTag>();
            foreach (var tag in db.Tags.Include("Articles").Where(x => x.Visibility && langugaes.Contains(x.Language)).OrderBy(x => x.Sort))
            {
                Tags.Add(new ViewTag(tag, tag.Articles.Count(x => x.Visibility && langugaes.Contains(x.Language))));
            }
            Categories = new List<ViewArticleCategory>();
            foreach (var cat in db.ArticleCategories.Include("Articles").Where(x => x.Visibility && currentLang == x.Language).OrderBy(x => x.Sort))
            {
                Categories.Add(new ViewArticleCategory(cat, cat.Articles.Count(x => x.Visibility && langugaes.Contains(x.Language))));
            }
        }

        private string[] GetAccessableLanguagesForTags(string language)
        {
            return LocalEntity.EN == language ? new[] { LocalEntity.EN } : new[] { LocalEntity.EN, language };
        }
    }
}