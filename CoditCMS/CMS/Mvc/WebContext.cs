using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DB.Entities;
using Libs;
using CMS.ViewModels;
using DB.Entities.Mocks;
using CMS.Core;
using DB.DAL;


namespace CMS.Mvc
{
    public class WebContext : IWebContext
    {
        private Location _location;
        private bool _checked = false;
        public Location Location
        {
            get
            {
                if (_location == null && !_checked)
                {
                    _checked = true;
                    using (var db = new ApplicationDbContext())
                    {
                        var locations = db.Location.ToList();
                        string alias = null;
                        var route = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
                        if (route != null)
                        {
                            var r = (Route)route.Route;
                            alias = (string)r.Defaults["location"];
                            if (string.IsNullOrEmpty(alias))
                            {
                                alias = (string)r.Defaults["controller"];
                            }
                        }
                        if (!string.IsNullOrEmpty(alias))
                        {
                            _location = locations.FirstOrDefault(l => l.Visibility && String.Equals(l.Alias.ToLower(), alias.ToLower(), StringComparison.InvariantCulture));
                        }
                    }
                }
                return _location ?? new Location();
            }
            set { _location = value; }
        }

        private IMetadataEntity _defaultMetadata;
        public IMetadataEntity Metadata
        {
            get
            {
                return _metadata ?? _defaultMetadata ?? (_defaultMetadata = new CustomMetadata
                {
                    MetaDescription = Location.MetaDescription,
                    MetaKeywords = Location.MetaKeywords,
                    MetaTitle = Location.MetaTitle ?? Location.Name
                });
            }
            set { _metadata = value; }
        }

        private ViewDataDictionary _viewData;
        private DynamicViewDataDictionary _dynamicViewData;
        private IMetadataEntity _metadata;

        public dynamic ViewBag
        {
            get { return _dynamicViewData ?? (_dynamicViewData = new DynamicViewDataDictionary(() => ViewData)); }
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

        public dynamic Pages
        {
            get { return DependencyResolver.Current.GetService<Pages>(); }
        }

        private List<BreadcrumbsViewModel> breadcrumbs = new List<BreadcrumbsViewModel>();

        public void PushBreadcrumb<T>(ItemViewModel<T> item) where T : IHaveAliasEntity
        {
            PushBreadcrumb(item.Item.Name, item.Item.Alias);
        }

        public void PushBreadcrumb(string name, string alias)
        {
            if (!breadcrumbs.Any(model => model.Alias == alias))
            {
                breadcrumbs.Add(new BreadcrumbsViewModel { Text = name, Alias = alias });
            }
        }

        public IEnumerable<BreadcrumbsViewModel> GetBreadcrumb()
        {
            return breadcrumbs.ToArray();
        }

        public void ClearBreadcrumb()
        {
            breadcrumbs.Clear();
        }
    }

    public interface IWebContext : IDotOrgWebContext
    {
        Location Location { get; set; }
        dynamic Pages { get; }
        void PushBreadcrumb(string name, string alias);
        void PushBreadcrumb<T>(ItemViewModel<T> item) where T : IHaveAliasEntity;
        IEnumerable<BreadcrumbsViewModel> GetBreadcrumb();
        void ClearBreadcrumb();
    }
}