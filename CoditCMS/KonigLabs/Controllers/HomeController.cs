using KonigLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var landing = new LandingPage() { Members = new List<Member>() };
                foreach(var member in db.CrewMembers.Include("Files").ToList())
                {
                    landing.Members.Add(new Member(member));
                }
                return View( landing);
            }
        }
    }
}