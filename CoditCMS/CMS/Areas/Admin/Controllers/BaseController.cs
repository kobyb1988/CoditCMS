using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.Entities;
using CMS.Controllers;
using DB.DAL;


namespace CMS.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, superadmin")]
    public class BaseController<T> : GenericController<T, ApplicationDbContext> where T : class, IEntity, new()
    {
    }
}
