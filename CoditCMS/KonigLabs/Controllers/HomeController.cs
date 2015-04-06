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
            using (var db = ApplicationDbContext.Create())
            {                
                var landing = new LandingPage(language, db);
                return View(landing);
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
    }
}