﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using CMS.Mvc;
    using CMS.PagesSettings.Lists;
    using CMS.ViewModels;
    using DB.Entities;
    using Libs;
    
    #line 1 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
    using CMS;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/Form/FormButtons.cshtml")]
    public partial class _Areas_Admin_Views_Shared_Form_FormButtons_cshtml : CMS.Mvc.AdminViewPage<dynamic>
    {
        public _Areas_Admin_Views_Shared_Form_FormButtons_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"form-save-buttons\"");

WriteLiteral(">\r\n    <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" id=\"save-button\"");

WriteLiteral(" name=\"_save\"");

WriteLiteral(" class=\"save-button\"");

WriteLiteral(" value=\"Сохранить\"");

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" id=\"apply-button\"");

WriteLiteral(" class=\"apply-button\"");

WriteLiteral(" name=\"_apply\"");

WriteLiteral(" value=\"Применить\"");

WriteLiteral(" id=\"_apply\"");

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ozi_backlink\"");

WriteAttribute("value", Tuple.Create(" value=\"", 304), Tuple.Create("\"", 333)
            
            #line 5 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
, Tuple.Create(Tuple.Create("", 312), Tuple.Create<System.Object, System.Int32>(WebContext.ReturnUrl
            
            #line default
            #line hidden
, 312), false)
);

WriteLiteral("/>\r\n");

            
            #line 6 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
    
            
            #line default
            #line hidden
            
            #line 6 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
     if (WebContext.ReturnUrl != null)
    {

            
            #line default
            #line hidden
WriteLiteral("        <span");

WriteLiteral(" class=\"form-back-link\"");

WriteLiteral("><span");

WriteLiteral(" class=\"arrow\"");

WriteLiteral(">&larr;</span><a");

WriteAttribute("href", Tuple.Create(" href=\"", 457), Tuple.Create("\"", 485)
            
            #line 8 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
, Tuple.Create(Tuple.Create("", 464), Tuple.Create<System.Object, System.Int32>(WebContext.ReturnUrl
            
            #line default
            #line hidden
, 464), false)
);

WriteLiteral(">Вернуться</a></span>\r\n");

            
            #line 9 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
    }
    else
    {

            
            #line default
            #line hidden
WriteLiteral("        <span");

WriteLiteral(" class=\"form-back-link\"");

WriteLiteral("><span");

WriteLiteral(" class=\"arrow\"");

WriteLiteral(">&larr;</span><a");

WriteAttribute("href", Tuple.Create(" href=\"", 605), Tuple.Create("\"", 638)
            
            #line 12 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
, Tuple.Create(Tuple.Create("", 612), Tuple.Create<System.Object, System.Int32>(Url.Action(Actions.Index)
            
            #line default
            #line hidden
, 612), false)
);

WriteLiteral(">Назад к списку</a></span>\r\n");

            
            #line 13 "..\..\Areas\Admin\Views\Shared\Form\FormButtons.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>");

        }
    }
}
#pragma warning restore 1591
