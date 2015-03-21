using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using DB.Entities;
using Libs;
using DB.DAL;

namespace CMS.Core
{
    public class Pages : DynamicObject
    {
        public static readonly string PageAlias = "page";
        private readonly IEnumerable<Location> _locations;

        public Pages()
        {
            using (var db = new ApplicationDbContext())
            {
                _locations = db.Location.ToList();
            }
            //var db = DependencyResolver.Current.GetService<DataModelContext>();
            //_locations = db.CreateObjectSet<Location>().ToList();
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _locations.Select(location => location.Alias.Capitalize());
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            throw new InvalidOperationException("Location cannot be set through the web context");
        }

        public dynamic this[string alias]
        {
            get
            {
                return _locations.FirstOrDefault(location => location.Alias.ToLower() == alias.ToLower());
            }
        }
    }
}