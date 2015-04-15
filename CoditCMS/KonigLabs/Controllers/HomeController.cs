using KonigLabs.Models;
using Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity;


namespace KonigLabs.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index(string language)
        {

            if (String.IsNullOrEmpty(language))
            {
                language = LocalEntity.RU;
            }
            var viewPath = "~/Views/Home/Index_{0}.cshtml";
            string view;
            switch (language)
            {
                case LocalEntity.RU:
                    view = String.Format(viewPath, language);
                    break;
                case LocalEntity.EN:
                    view = String.Format(viewPath, language);
                    break;
                default:
                    view = String.Format(viewPath, LocalEntity.RU);
                    break;
            }
            using (var db = ApplicationDbContext.Create())
            {
                var landing = new LandingPage(language, db);
                return View(view, landing);
            }
        }


        public virtual ActionResult Member(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var member = db.CrewMembers.Where(m => m.Id == id).FirstOrDefault();
                if (member == null)
                {
                    throw new HttpException(404, "NotFound");
                }
                var vm = new ViewMember(member);
                return View("~/Views/Shared/DisplayTemplates/MemberBio.cshtml", vm);
            }
        }

        public virtual ActionResult Project(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var project = db.Projects.Where(p => p.Id == id).FirstOrDefault();
                if (project == null)
                {
                    throw new HttpException(404, "NotFound");
                }
                var vm = new ViewProject(project);
                return View("~/Views/Shared/DisplayTemplates/ProjectDescr.cshtml", vm);
            }
        }

        public virtual ActionResult Article(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var article = db.Articles.Where(m => m.Id == id).FirstOrDefault();
                if (article == null)
                {
                    throw new HttpException(404, "NotFound");
                }
                var vm = new ViewArticle(article);
                return View("~/Views/Shared/DisplayTemplates/ArticleFull.cshtml", vm);
            }
        }

        public ActionResult Projects(string language)
        {
            if (String.IsNullOrEmpty(language))
            {
                language = LocalEntity.RU;
            }
            using (var db = ApplicationDbContext.Create())
            {
                return View("~/Views/Shared/DisplayTemplates/MoreProjects.cshtml", new ViewProjects(language, db));
            }
        }

        [HttpPost]
        public virtual ActionResult Contact(ViewContact contact)
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
                contact.Status = "Спасибо за ваше сообщение, мы обязательно свяжемся с вами!";

                var message = new MailMessage();
                var toEmail = "aganzha@yandex.ru";

                message.To.Add(new MailAddress(toEmail));
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = "New message";

                var sb = new StringBuilder();
                sb.AppendFormat("<p>{0}</p>", contact.Name);
                sb.AppendFormat("<p>{0}</p>", contact.Text);
                sb.AppendFormat("<p>{0}</p>", contact.Email);
                sb.AppendFormat("<p>{0}</p>", contact.Phone);

                message.Body = sb.ToString();

                var client = new SmtpClient();
                if (client.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
                {
                    client.EnableSsl = false;
                }

                try
                {
                    client.Send(message);
                    client.SendCompleted += (s, e) =>
                    {
                        message.Dispose();
                        client.Dispose();
                    };
                }
                catch (Exception exc)
                {

                }

            }
            else
            {
                contact.Status = "Что-то пошло не так :(";
            }
            return View(contact);
        }

        public virtual ActionResult BlogPost(int id)
        {
            using (var db = ApplicationDbContext.Create())
            {
                var article = db.Articles.Include(a=>a.Files).Include(a=>a.Tags).Include(a=>a.Categories).Include(a=>a.CrewMember)
                    .Include(a => a.CrewMember.Files)
                    .Where(a => a.Id == id).FirstOrDefault();
                ViewBag.BlogMeta = new BlogMeta(db);                
                return View(article);
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

        public virtual ActionResult Blog(string language, int? page, int? tag, int? cat, string search)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));
            if (String.IsNullOrEmpty(language))
            {
                language = LocalEntity.RU;
            }
            using (var db = ApplicationDbContext.Create())
            {

                var articles = KonigLabs.Models.Article.GetArticles(language, db);
                if (cat.HasValue)
                {
                    var category = db.ArticleCategories.Where(c => c.Id == cat.Value).FirstOrDefault();
                    if (category != null)
                    {
                        var ids = category.Articles.Select(a => a.Id).ToList();
                        articles = articles.Where(a => ids.Contains(a.Id));
                    }
                }
                if (tag.HasValue)
                {
                    var _tag = db.Tags.Where(t => t.Id == tag.Value).FirstOrDefault();
                    if (_tag != null)
                    {
                        var ids = _tag.Articles.Select(a => a.Id).ToList();
                        articles = articles.Where(a => ids.Contains(a.Id));
                    }
                }
                if (!String.IsNullOrEmpty(search))
                {
                    if (!StopWords.Contains(search))
                    {
                        articles = articles.Where(a => a.Content.ToLower().Contains(search.ToLower()));
                    }
                }
                var list = articles.ToPagedList(pageNumber, pageSize);
                var bm = new BlogMeta(db);
                bm.SearchValue = search;
                ViewBag.BlogMeta = bm;
                return View(list);
            }
        }
    }

}