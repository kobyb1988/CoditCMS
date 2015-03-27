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
                    Projects=new List<ViewProject>(),
                    Categories = new List<ViewCategory>(),
                    Clients = new List<ViewClient>(),
                    Articles = new List<ViewArticle>()
                };

                foreach(var member in db.CrewMembers.Include("Files").ToList())
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

                return View( landing);
            }
        }
    }
}