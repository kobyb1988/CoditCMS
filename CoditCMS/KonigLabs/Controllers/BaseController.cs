using System;
using System.Web.Mvc;
using KonigLabs.Core.ServiceProviderIml;
using KonigLabs.Models;
using Libs.Services;

namespace KonigLabs.Controllers
{
    public partial class BaseController : Controller
    {
        protected ILocalizationProvider _lang = new Lang();

        protected ViewResult LocalizableView(string viewPath,object model)
        {
            var language = _lang.GetLanguageName();

            string localizeViewPath;
            switch (language)
            {
                case LocalEntity.RU:
                    localizeViewPath = String.Format(viewPath, language);
                    break;
                case LocalEntity.EN:
                    localizeViewPath = String.Format(viewPath, language);
                    break;
                default:
                    localizeViewPath = String.Format(viewPath, LocalEntity.RU);
                    break;
            }
            return View(localizeViewPath,model);
        }
    }
}