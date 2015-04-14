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
                var article = db .Articles.Where(m => m.Id == id).FirstOrDefault();
                if (article == null)
                {
                    throw new HttpException(404, "NotFound");
                }
                var vm = new ViewArticle(article);
                return View("~/Views/Shared/DisplayTemplates/ArticleFull.cshtml", vm);
            }
        }

        public ActionResult Projects()
        {
            return View("~/Views/Shared/DisplayTemplates/MoreProjects.cshtml");
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
    }
}