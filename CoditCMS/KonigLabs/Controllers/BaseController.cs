using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.Entities;
using KonigLabs.Core;
using KonigLabs.Core.ServiceProviderIml;
using KonigLabs.Models;
using Libs.Services;
using ScientiaMobile.WurflCloud;
using ScientiaMobile.WurflCloud.Config;

namespace KonigLabs.Controllers
{
    public partial class BaseController : Controller
    {

        protected ILocalizationProvider _lang = new Lang();
        public string CurentLang { get { return _lang.GetLanguageName(); } }
        public string[] AccessableLanguagesForTags
        {
            get
            {
                return GetAccessableLanguagesForTags(_lang.GetLanguageName());
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.DeviceInfo = GetDataByRequest(HttpContext);
            base.OnActionExecuting(filterContext);
        }

        public DeviceInfo GetDataByRequest(HttpContextBase context)
        {
            var config = new DefaultCloudClientConfig
            {
                ApiKey = "869109:KECpUB4sJ1jePSvXci95gINah6zQrkmx"
            };

            var manager = new CloudClientManager(config);

            // Grab data
            var info = manager.GetDeviceInfo(context);

            return info.ConvertToKonigLabsDiviceInfo();
        }

        protected ViewResult LocalizableView(string viewPath, object model)
        {
            var language = _lang.GetLanguageName();

            string localizeViewPath;
            switch (language)
            {
                case LocalEntity.RU:
                    localizeViewPath = String.Format(viewPath, (Request.Browser.IsMobileDevice || ((DeviceInfo)ViewBag.DeviceInfo).IsTablet) ? language + ".mobile" : language);
                    break;
                case LocalEntity.EN:
                    localizeViewPath = String.Format(viewPath, (Request.Browser.IsMobileDevice || ((DeviceInfo)ViewBag.DeviceInfo).IsTablet) ? language + ".mobile" : language);
                    break;
                default:
                    localizeViewPath = String.Format(viewPath, LocalEntity.RU);
                    break;
            }
            return View(localizeViewPath, model);
        }

        private string[] GetAccessableLanguagesForTags(string language)
        {
            return LocalEntity.EN == language ? new[] { LocalEntity.EN } : new[] { LocalEntity.EN, language };
        }

        protected IQueryable<T> GetEntities<T>() where T : class
        {
            var db = ApplicationDbContext.Create();
            var list = db.Set<T>();
            IQueryable<T> result = list;
            if (typeof(ILocalizableEntity).IsAssignableFrom(typeof(T)))
            {
                var l = DependencyResolver.Current.GetService<ILocalizationProvider>();
                var lang = l.GetLanguageName();
                result = list.ToList().Where(arg => ((ILocalizableEntity)arg).Lang == lang).AsQueryable();
            }
            return result;
        }

        protected IQueryable<T> GetSortedVisible<T>(int? page = null, int pageSize = 0) where T : class, IVisibleEntity
        {
            var result = GetEntities<T>().Where(arg => arg.Visibility);
            if (typeof(ISortableEntity).IsAssignableFrom(typeof(T)))
            {
                result = result.ToList().OrderBy(arg => ((ISortableEntity)arg).Sort).AsQueryable();
            }
            if (page.HasValue)
            {
                result = result.Skip((page.Value - 1) * pageSize).Take(pageSize);
            }
            return result;
        }
    }
}