using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using CMS.PagesSettings;
using CMS.PagesSettings.Forms;
using DB.Entities;

namespace CMS.Mvc
{
    public class AdminWebContext
    {
        public AdminWebContext()
        {

        }

        // aganhza
        private static object syncRoot = new Object();
        private static AdminWebContext _context { get; set; }
        public static AdminWebContext GetContext()
        {
            if (_context == null)
            {
                lock (syncRoot)
                {
                    _context = new AdminWebContext();
                }
            }
            return _context;
        }

        private Settings _settings;
        private string _returnUrl;

        public string ReturnUrl
        {
            get { return _returnUrl ?? HttpContext.Current.Request["returnUrl"] ?? HttpContext.Current.Request["ozi_backlink"]; }
            set { _returnUrl = value; }
        }

        public int? PrevId { get; set; }
        public int? NextId { get; set; }

        public bool IsCreate { get; set; }

        public string EditViewName { get; set; }

        public Settings GetSettings(Type controllerType = null)
        {
            if (_settings == null)
            {
                if (controllerType != null)
                {
                    var name = GetSettingsName(controllerType);
                    _settings = new Settings(name);
                }
            }
            return _settings;
        }

        private string GetSettingsName(Type controllerType)
        {
            Debug.Assert(controllerType.BaseType != null);
            return controllerType.BaseType.GetGenericArguments()[0].Name;
        }

        private ViewDataDictionary _viewData;
        private Libs.DynamicViewDataDictionary _dynamicViewData;

        public dynamic ViewBag
        {
            get { return _dynamicViewData ?? (_dynamicViewData = new Libs.DynamicViewDataDictionary(() => ViewData)); }
        }

        protected virtual void SetViewData(ViewDataDictionary viewData)
        {
            _viewData = viewData;
        }

        public ViewDataDictionary ViewData
        {
            get
            {
                if (_viewData == null)
                {
                    SetViewData(new ViewDataDictionary());
                }
                return _viewData;
            }
            set { SetViewData(value); }
        }

        public FieldSettings FieldSettings { get; set; }

        public TabsSettings CurrentTab { get; set; }

        public string HtmlPageTitle { get; set; }

        public IEntity Model { get; set; }

        public Type ModelType { get; set; }
    }
}