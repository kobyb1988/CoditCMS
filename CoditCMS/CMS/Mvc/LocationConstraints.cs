using DB.DAL;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.Mvc
{
    public class LocationConstraints : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            using (var db = ApplicationDbContext.Create())
            {
                try
                {
                    var locations = db.Location.ToList();
                    //var webContext = AdminWebContext.GetContext();
                    var webContext = DependencyResolver.Current.GetService<IWebContext>();

                    if (routeDirection == RouteDirection.IncomingRequest)
                    {
                        var path = (string)values["location"];
                        var result = CheckIncomingRouteValues(path, locations);
                        webContext.Location = result;
                        return result != null;
                    }

                    if (routeDirection == RouteDirection.UrlGeneration)
                    {
                        return true;
                    }
                }
                catch
                {
                }
                return false;
            }
        }

        private Location CheckIncomingRouteValues(string path, IList<Location> locations)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            var parts = path.Split('/').Select(s => s.ToLower()).ToList();
            return CheckParts(parts, locations);
        }

        private Location CheckParts(IList<string> parts, IList<Location> locations)
        {
            var page = locations.FirstOrDefault(l => l.Visibility && l.Alias == parts[0]);
            return page != null ? CheckPartsRecursively(page, parts, 1, locations) : null;
        }

        private Location CheckPartsRecursively(Location current, IList<string> parts, int index, IList<Location> locations)
        {
            if (index >= parts.Count)
                return current;
            var page = locations.FirstOrDefault(l => l.Visibility && l.ParentId == current.Id && l.Alias == parts[index]);
            return page != null ? CheckPartsRecursively(page, parts, index + 1, locations) : null;
        }

    }
}
