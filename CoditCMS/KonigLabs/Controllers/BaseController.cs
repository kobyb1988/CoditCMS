﻿using System;
using System.Linq;
using System.Web.Mvc;
using DB.Entities;
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