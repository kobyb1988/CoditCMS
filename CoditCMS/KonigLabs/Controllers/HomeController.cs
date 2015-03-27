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

                var p = db.Projects.FirstOrDefault();
                
                p.ProjectCategory.Clear();

                //var type = TypeHelpers.GetPropertyType(p, "ProjectCategory");
                var properety = TypeHelpers.GetPropertyValue(p, "ProjectCategory");
                var type = properety.GetType();
                MethodInfo methodInfo = type.GetMethod("Clear");
                //try
                //{
                //    MethodInfo methodInfo = type.GetMethod("Clear");

                
                var landing = new LandingPage() { Members = new List<ViewMember>() };
                foreach(var member in db.CrewMembers.Include("Files").ToList())
                {
                    landing.Members.Add(new ViewMember(member));
                }
                return View( landing);
            }
        }
    }
}