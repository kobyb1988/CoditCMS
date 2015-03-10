using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using CMS.Mvc;
using Libs;

namespace CMS.Controllers
{
    public abstract partial class CoditController : Controller
    {
        protected string L(string key)
        {
            return key.Localize();
        }

        public virtual DbContext DataModelContext { get { return null; } }

        [NonAction]
        protected virtual ActionResult ToIndex()
        {
            return RedirectToAction(Constants.IndexView);
        }


        [NonAction]
        protected ActionResult Back()
        {
            if (Request.UrlReferrer == null)
                return ToIndex();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        
        public virtual ActionResult SetLanguage(string l)
        {
            var route = UriHelper.GetRoute(Request.UrlReferrer);
            if (l == "ru")
                l = null;
            if (l != null)
            {
                route.Values["lang"] = l;
            }
            else
            {
                route.Values.Remove("lang");
            }
            var url = Url.RouteUrl(route.Values);

            Response.Cookies.Add(new HttpCookie("language"));
            return Redirect(url);
        }
    }
}
