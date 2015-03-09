using System.Web.Mvc;
using Libs.Services;

namespace Libs
{
	public static class LocalizationHelpers
	{
		public static string Localize(this string key)
		{
			var lang = DependencyResolver.Current.GetService<ILocalizationProvider>();
			return lang.GetMessage(key);
		}
	}
}