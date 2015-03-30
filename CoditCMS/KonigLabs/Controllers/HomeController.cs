using KonigLabs.Models;
using Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace KonigLabs.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {

            using (var db = ApplicationDbContext.Create())
            {


                var landing = new LandingPage()
                {
                    Members = new List<ViewMember>(),
                    Projects = new List<ViewProject>(),
                    Categories = new List<ViewCategory>(),
                    Clients = new List<ViewClient>(),
                    Articles = new List<ViewArticle>()
                };

                foreach (var member in db.CrewMembers.Include("Files").ToList())
                {
                    landing.Members.Add(new ViewMember(member));
                }

                var cats = new Dictionary<string, ViewCategory>();

                foreach (var project in db.Projects.Include("ProjectCategory").Include("Files").ToArray())
                {
                    var viewProject = new ViewProject(project);
                    landing.Projects.Add(viewProject);
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
                landing.Categories = cats.Values.ToList();

                foreach (var cl in db.Clients.Include("Files").ToArray())
                {
                    landing.Clients.Add(new ViewClient(cl));
                }


                foreach (var ar in db.Articles.Include("Files").ToArray())
                {
                    landing.Articles.Add(new ViewArticle(ar));
                }
                landing.Contact = new ViewContact();

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
                contact.Name = "";
                contact.Email = "";
                contact.Phone = "";
                contact.Text = "";

            }
            else
            {
                contact.Status = "Что-то пошло не так :(";
            }
            return View(contact);
        }
    }
}