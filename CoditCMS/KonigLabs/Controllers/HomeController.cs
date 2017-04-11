﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using KonigLabs.Models;
using Libs;
using PagedList;

namespace KonigLabs.Controllers
{
    public partial class HomeController : BaseController
    {
        public virtual ActionResult Index()
        {
            using (var db = ApplicationDbContext.Create())
            {
                var landing = new LandingPage(_lang.GetLanguageName(), db);
                return LocalizableView("~/Views/Home/Index_{0}.cshtml", landing);
            }
        }

        public virtual ActionResult Member(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var member = db.CrewMembers.FirstOrDefault(m => m.Id == id);
                if (member == null)
                {
                    Response.StatusCode = 404;
                    return View("NotFound");
                }
                var vm = new ViewMember(member);
                return PartialView("~/Views/Shared/DisplayTemplates/MemberBio.cshtml", vm);
            }
        }

        public virtual ActionResult Project(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var project = db.Projects.FirstOrDefault(p => p.Id == id);
                if (project == null)
                {
                    Response.StatusCode = 404;
                    return View("NotFound");
                }
                var vm = new ViewProject(project);
                return PartialView("~/Views/Shared/DisplayTemplates/ProjectDescr.cshtml", vm);
            }
        }

        public virtual ActionResult Article(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var article = db.Articles.FirstOrDefault(m => m.Id == id);
                if (article == null)
                {
                    Response.StatusCode = 404;
                    return View("NotFound");
                }
                var vm = new ViewArticle(article);
                return View("~/Views/Shared/DisplayTemplates/ArticleFull.cshtml", vm);
            }
        }

        public virtual ActionResult Projects(int page)
        {

            var language = LocalEntity.RU;
            var itemOnPage = 3;
            using (var db = ApplicationDbContext.Create())
            {
                var landing = new LandingPage(_lang.GetLanguageName(), db);
                var totalItems = landing.Projects;
                var projects = totalItems.Skip(itemOnPage * page).Take(itemOnPage + 1).ToList();
                return PartialView("~/Views/Shared/DisplayTemplates/MoreProjects.cshtml", new ViewProjects(language, db) { Projects = projects, TotalAmountOfProjects = totalItems.Count });
            }
        }

        [HttpGet]
        public virtual ActionResult GetProjects(int page)
        {
            var itemOnPage = 3;
            using (var db = ApplicationDbContext.Create())
            {
                var landing = new LandingPage(_lang.GetLanguageName(), db);
                var projects = landing.Projects;
                if (Request != null)
                {
                    if (Request.IsAjaxRequest())
                    {
                        projects = projects.Skip(itemOnPage * page).Take(itemOnPage).ToList();
                        return PartialView("~/Views/Shared/DisplayTemplates/ProjectList.cshtml", projects);
                    }
                }

                projects = projects.Take(itemOnPage).ToList();
                return PartialView(projects);
            }
        }

        [HttpPost]
        public async virtual Task<ActionResult> Contact(ViewContact contact)
        {
            if (ModelState.IsValid)
            {

                using (var db = ApplicationDbContext.Create())
                {
                    var dbContact = new Contact();
                    dbContact.Date = DateTime.Now;
                    dbContact.Name = contact.Name;
                    dbContact.Email = contact.Email;
                    dbContact.Phone = contact.Phone;
                    dbContact.Text = contact.Text;
                    db.Contacts.Add(dbContact);
                    db.SaveChanges();
                }
                contact.Status = "true";
                Response.StatusCode = 201;

                var sb = new StringBuilder();
                sb.AppendFormat("<p>{0}</p>", contact.Name);
                sb.AppendFormat("<p>{0}</p>", contact.Text);
                sb.AppendFormat("<p>{0}</p>", contact.Email);
                sb.AppendFormat("<p>{0}</p>", contact.Phone);

                await Task.Factory.StartNew(() => Tasks.SendEmailToAdmin(sb.ToString()));
            }
            else
            {
                contact.Status = "flase";
            }
            return LocalizablePartialView("~/Views/Home/Contact_{0}.cshtml", contact);
        }

        public virtual ActionResult BlogPost(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var article = db.Articles.Include(a => a.Files).Include(a => a.Tags)
                    .Include(a => a.Categories)
                    .Include(a => a.CrewMember).Include(a => a.CrewMember.Files)
                    .Include(a => a.Comments)
                    .Include(a => a.Comments.Select(c => c.CrewMember)).FirstOrDefault(a => a.Id == id);
                if (article == null)
                {
                    Response.StatusCode = 404;
                    return View("NotFound");
                }
                int commentCount = 0;
                foreach (var comment in article.Comments.Where(c => c.Parent == null && c.Visibility))
                {
                    comment.WalkDawn(1);
                    commentCount += comment.CommentCount + 1;
                }
                ViewBag.BlogMeta = new BlogMeta(db, AccessableLanguagesForTags, _lang.GetLanguageName());
                ViewBag.CommentCount = commentCount;
                return LocalizableView("~/Views/Home/BlogPost_{0}.cshtml", article);
            }
        }


        public static string[] StopWords = new[] {
           "а", "без", "более", "бы", "был", "была", "были","один", "два", "три", "четыре", "пять", "шесть", "семь","восемь",
           "было", "быть", "в", "вам", "вас", "весь", "во", "вот", "все", "всего", "всех", "вы", "где", "да", "даже", "для",
           "до", "его", "ее", "если", "есть", "еще", "же", "за", "здесь", "и", "из", "из-за", "или", "им", "их", "к", "как",
           "как-то", "ко", "когда", "кто", "ли", "либо", "мне", "может", "мы", "на", "надо", "наш", "не", "него", "нее", "нет",
           "ни", "них", "но", "ну", "о", "об", "однако",
           "1","2","3","4","5","6","7","8","9","0", "он", "она", "они", "интернет", "сайт", "вэб",
           "сайт", "вопрос","оно", "от", "очень", "по", "под", "при", "с", "со", "так",
            "также", "такой", "там", "те", "тем", "то", "того", "тоже", "той", "только", "том", "ты", "у", "уже",
            "хотя", "чего", "чей", "чем", "что", "чтобы", "ответ", "чье", "чья", "эта", "эти", "это", "я"
        };

        public virtual ActionResult Blog(int? page, int? tag, int? cat, int? authorId, string search)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            using (var db = ApplicationDbContext.Create())
            {

                var articles = Models.Article.GetArticles(AccessableLanguagesForTags, db);
                if (cat.HasValue)
                {
                    var category = db.ArticleCategories.FirstOrDefault(c => c.Id == cat.Value
                                                                            && c.Visibility
                                                                            && AccessableLanguagesForTags.Contains(c.Language));
                    if (category != null)
                    {
                        var ids = category.Articles.Select(a => a.Id).ToList();
                        articles = articles.Where(a => ids.Contains(a.Id));
                    }
                }
                if (tag.HasValue)
                {
                    var _tag = db.Tags.FirstOrDefault(t => t.Id == tag.Value
                                                        && t.Visibility
                                                        && AccessableLanguagesForTags.Contains(t.Language));
                    if (_tag != null)
                    {
                        var ids = _tag.Articles.Select(a => a.Id).ToList();
                        articles = articles.Where(a => ids.Contains(a.Id));
                    }
                }
                if (authorId.HasValue)
                    articles = articles.Where(x => x.CrewMemberId == authorId);
                if (!String.IsNullOrEmpty(search))
                {
                    if (!StopWords.Contains(search))
                    {
                        articles = articles.Where(a => a.Content.ToLower().Split(' ').ToList().
                            Any(x => search.ToLower().Split(' ').Contains(x))
                            || a.Title.ToLower().Split(' ').ToList().
                            Any(x => search.ToLower().Split(' ').Contains(x)));
                    }
                }
                var list = articles.ToPagedList(pageNumber, pageSize);
                var bm = new BlogMeta(db, AccessableLanguagesForTags, _lang.GetLanguageName()) { SearchValue = search };
                ViewBag.BlogMeta = bm;
                return LocalizableView("~/Views/Home/Blog_{0}.cshtml", list);
            }
        }


        public async virtual Task<ActionResult> Comment(string name, string email, string text, int? commentId, int? postId)
        {

            using (var db = ApplicationDbContext.Create())
            {
                Article article = null;
                Comment comment = null;
                if (postId.HasValue)
                {
                    article = db.Articles.FirstOrDefault(a => a.Id == postId.Value);
                }
                else
                {
                    if (commentId.HasValue)
                    {
                        comment = db.Comments.FirstOrDefault(c => c.Id == commentId.Value);
                        if (comment != null)
                        {
                            article = comment.Article;
                        }
                    }
                }
                if (article != null)
                {
                    var c = new Comment();
                    c.Name = name;
                    c.Email = email;
                    c.Article = article;
                    c.Content = text;
                    c.Date = DateTime.Now;
                    if (comment != null)
                    {
                        c.Parent = comment;
                    }
                    db.Comments.Add(c);
                    db.SaveChanges();

                    var sb = new StringBuilder();
                    sb.AppendFormat("<p>{0}</p>", c.Name);
                    sb.AppendFormat("<p>{0}</p>", c.Content);
                    sb.AppendFormat("<p>{0}</p>", c.Email);

                    await Task.Factory.StartNew(() => Tasks.SendEmailToAdmin(sb.ToString()));
                }
                return Content("ok");
            }
        }


        public virtual ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }


    }

}