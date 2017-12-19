using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KonigLabs.Controllers
{
    public partial class PolicyController : BaseController
    {
        // GET: Policy
        public virtual ActionResult PrivatePolicyAgreement()
        {
            return LocalizableView("~/Views/Policy/PrivatePolicyAgreement_{0}.cshtml", null);
        }

        public virtual ActionResult PersonalDataAgreement()
        {
            return LocalizableView("~/Views/Policy/PersonalDataAgreement_{0}.cshtml", null);
        }
    }
}