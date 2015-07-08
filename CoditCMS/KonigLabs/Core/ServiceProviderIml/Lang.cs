using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using KonigLabs.Models;
using Libs.Services;

namespace KonigLabs.Core.ServiceProviderIml
{
    public class Lang : ILocalizationProvider
    {
        public static ILocalizationProvider Instance
        {
            get { return DependencyResolver.Current.GetService<ILocalizationProvider>(); }
        }

        #region ILocalizationProvider Members

        string ILocalizationProvider.GetMessage(string key)
        {
            return StaticLanguageValues.GetValue(key);
        }

        string ILocalizationProvider.GetMessage(Guid? key)
        {
            return key.HasValue ? GetValue(key.Value) : string.Empty;
        }

        public void Reset()
        {
            _dict = null;
        }

        private static volatile Dictionary<string, Dictionary<Guid, string>> _dict;

        private string GetValue(Guid key)
        {
            throw new NotImplementedException();
        }

        CultureInfo ILocalizationProvider.GetCulture()
        {
            return CultureInfo.GetCultureInfo(((ILocalizationProvider)this).GetLanguageName());
        }

        private string _current;

        string ILocalizationProvider.GetLanguageName()
        {
            if (string.IsNullOrEmpty(_current))
            {
                string lang = "ru";

                try
                {
                    var route = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
                    if (route != null)
                    {
                        lang = (string)route.Values["lang"];
                    }
                }
                catch (LockRecursionException)
                {
                    // это означает что мы находимся в формировании таблицы роутинга, поэтому надо взять язык из урла
                    var url = HttpContext.Current.Request.Url.AbsolutePath;
                    lang = Languages.FirstOrDefault(l => url.StartsWith("/" + l));
                }
         
                _current = Languages.Contains(lang) || lang == "test" ? lang.ToLowerInvariant() : "ru";
            }
            return _current;
        }

        private IEnumerable<string> _lang;

        public IEnumerable<string> Languages
        {
            get
            {
                return new[] { LocalEntity.RU, LocalEntity.EN };
            }
        }

        #endregion

        private static class StaticLanguageValues
        {
            private readonly static Dictionary<string, Dictionary<string, string>> Values = new Dictionary<string, Dictionary<string, string>>();

            private readonly static object StaticLockObject = new object();

            private static Dictionary<string, string> GetDictionaryByLang(string lang)
            {
                if (!Values.ContainsKey(lang))
                {
                    lock (StaticLockObject)
                    {
                        if (!Values.ContainsKey(lang))
                        {
                            var dict = new Dictionary<string, string>();
                            var doc = XDocument.Load(HttpContext.Current.Server.MapPath(string.Format("/App_Data/{0}.xml", lang)));
                            var xElement = doc.Element("messages");
                            if (xElement != null)
                            {
                                xElement.Elements("add").ToList().ForEach(element =>
                                {
                                    var key = element.Attribute("key").Value;
                                    if (!dict.ContainsKey(key))
                                    {
                                        dict.Add(key, element.Attribute("value").Value);
                                    }
                                    else
                                    {
                                        Debug.WriteLine(string.Format("Duplicate key {0}", key));
                                    }
                                });
                            }
                            Values.Add(lang, dict);
                        }
                    }
                }
                return Values[lang];
            }

            public static string GetValue(string key)
            {
                try
                {
                    var lang = Instance.GetLanguageName();
                    if (lang == "test")
                        return key;
                    var values = GetDictionaryByLang(lang);
                    return values[key];
                }
                catch
                {
                    return key;
                }
            }
        }
    }
}