// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
#pragma warning disable 1591, 3008, 3009, 0108
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace T4MVC
{
    public class SharedController
    {

        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _CoditLayout = "_CoditLayout";
                public readonly string _Layout = "_Layout";
                public readonly string _Layout_Mobile = "_Layout.Mobile";
                public readonly string _LoginPartial = "_LoginPartial";
                public readonly string Error = "Error";
                public readonly string Lockout = "Lockout";
                public readonly string NotFound = "NotFound";
                public readonly string old_Layout = "old_Layout";
            }
            public readonly string _CoditLayout = "~/Views/Shared/_CoditLayout.cshtml";
            public readonly string _Layout = "~/Views/Shared/_Layout.cshtml";
            public readonly string _Layout_Mobile = "~/Views/Shared/_Layout.Mobile.cshtml";
            public readonly string _LoginPartial = "~/Views/Shared/_LoginPartial.cshtml";
            public readonly string Error = "~/Views/Shared/Error.cshtml";
            public readonly string Lockout = "~/Views/Shared/Lockout.cshtml";
            public readonly string NotFound = "~/Views/Shared/NotFound.cshtml";
            public readonly string old_Layout = "~/Views/Shared/old_Layout.cshtml";
            static readonly _DisplayTemplatesClass s_DisplayTemplates = new _DisplayTemplatesClass();
            public _DisplayTemplatesClass DisplayTemplates { get { return s_DisplayTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _DisplayTemplatesClass
            {
                public readonly string Article_en = "Article_en";
                public readonly string Article_ru = "Article_ru";
                public readonly string ArticleFull = "ArticleFull";
                public readonly string ArticleInBlog_en = "ArticleInBlog_en";
                public readonly string ArticleInBlog_ru = "ArticleInBlog_ru";
                public readonly string BlogHeader_en = "BlogHeader_en";
                public readonly string BlogHeader_ru = "BlogHeader_ru";
                public readonly string BlogPane_en = "BlogPane_en";
                public readonly string BlogPane_ru = "BlogPane_ru";
                public readonly string Client = "Client";
                public readonly string Comment = "Comment";
                public readonly string Member = "Member";
                public readonly string Member_Mobile = "Member.Mobile";
                public readonly string MemberBio = "MemberBio";
                public readonly string MoreProjects = "MoreProjects";
                public readonly string Project = "Project";
                public readonly string ProjectDescr = "ProjectDescr";
            }
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                }
            }
        }
    }

}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108
